﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Modules.Book {
    
    public class BookMetaInfo {
        
        private static readonly char COLLAPSE_DELIMITER = '|';

        public string title { get; set; }
        public string author { get; set; }
        public string publisher { get; set; } 
        public string language { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string[] tags { get; set; }
        public DateTime publicationDate { get; set; }
        public int pageCount { get; set; }
        
        public BookMetaInfo() {}

        public BookMetaInfo(string title, string author, string publisher, string language, string description, 
            string category, string[] tags, DateTime publicationDate, int pageCount) {
            this.title = title;
            this.author = author;
            this.publisher = publisher;
            this.language = language;
            this.description = description;
            this.category = category;
            this.tags = tags;
            this.publicationDate = publicationDate;
            this.pageCount = pageCount;
        }

        public string collapseTags() {
            return String.Join(COLLAPSE_DELIMITER.ToString(), tags);
        }

        private List<string> splitTags(string lines) {
            return lines.Split(COLLAPSE_DELIMITER).ToList();
        }
    }
}
