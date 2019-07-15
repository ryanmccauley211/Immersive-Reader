﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Modules.EReader;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Modules.Bridge {
    
    public class Library {
        
        private static Library instance;
        private static readonly object padlock = new object();

        public List<Shelf> shelves { get; set; }
        public Dictionary<string, Book> books { get; set; }
        
        public string currentBookId { get; set; }
                
        public Library() {
            if (instance != null){
                throw new NotSupportedException("Only one instance of Library is allowed. Access via Library.Instance");
            }
        }

        public static Library Instance {
            get {
                lock(padlock) {
                    if (instance == null) {
                        initialize();
                        Deserialize();
                    }
                    return instance;
                }
            }
        }

        public void init() {
            Debug.Log("Library initialization requested...");
        }

        private static void initialize() {
            string libraryPath = Config.Instance.libraryPath;
            if (!File.Exists(libraryPath)) {
                Library library = new Library();
                library.shelves = new List<Shelf>();
                library.books = new Dictionary<string, Book>();
                library.serialize();
            }
        }
        
        public void serialize() {
            var serializer = new SerializerBuilder()
                .WithTagMapping("!basicBook", typeof(BasicBook))
                .WithTagMapping("!basicPage", typeof(BasicPage))
                .EmitDefaults()
                .DisableAliases()
                .Build();            
            var yaml = serializer.Serialize(Instance);
            File.WriteAllText(Config.Instance.libraryPath, yaml);
        }

        private static void Deserialize() {
            
            string libraryContent;
            FileStream fileStream = new FileStream(Config.Instance.libraryPath, FileMode.Open, FileAccess.Read);
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8)) {
                libraryContent = streamReader.ReadToEnd();
            }
            
            StringReader yamlInput = new StringReader(libraryContent);
            Deserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .WithTagMapping("!basicBook", typeof(BasicBook))
                .WithTagMapping("!basicPage", typeof(BasicPage))
                .Build();

            instance = deserializer.Deserialize<Library>(yamlInput);
        }

        public List<Book> retrieveAllBooks() {
            return books.Values.ToList();
        }

        public Book retrieveBook(string bookId) {
            if (!doesLibraryContainId(bookId)) {
                throw new BookNotFoundException("Unable to find book with id " + bookId);
            }
            return books[bookId];
        }

        public bool doesLibraryContainId(string bookId) {
            return books.ContainsKey(bookId);
        }
        
        public bool doesLibraryContainTitle(string title) {
            foreach(KeyValuePair<string, Book> book in books) {
                if (book.Value.bookMetaInfo.title == title) {
                    return true;
                }
            }
            return false;
        }

        public void addShelf(Shelf shelf) {
            shelves.Add(shelf);
        }
        
        public void addBook(Book book) {
            books.Add(book.bookId, book);
        }
    }

    class BookNotFoundException : Exception {
        
        public BookNotFoundException(){}

        public BookNotFoundException(string message): base(message){}
    }
}