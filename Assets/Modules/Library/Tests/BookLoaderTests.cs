﻿using NUnit.Framework;
using Modules.Bridge;
using Modules.Common;
using Modules.EReader;

namespace Modules.Bridge.Tests {

    public class BookLoaderTests {

        [Test]
        public void LoadingTextFilesAddBasicPages() {
            BookImporter<string[]> bookImporter = new BookImporter<string[]>();
            Book book = new BasicBook("Assets/Modules/Book/Tests/Resources/dracula.txt");

            bookImporter.loadDotText(book);
            Assert.Greater(book.getPageCount(), 0);
            Assert.AreEqual(typeof(TextPage), book.getAllPages()[0].GetType());
        }
        
        [Test]
        public void LoadingTextFilesGetAddedToLibrary() {
            BookImporter<string[]> bookImporter = new BookImporter<string[]>();
            Book book = new BasicBook("Assets/Modules/Book/Tests/Resources/dracula.txt");
            Assert.True(Library.Instance.doesLibraryContainId(book.bookId));
        }
        
        [Test]
        public void FileExtensionsExtractedCorrectly() {
            string ext = FileUtils.getFileExt("Assets/Modules/Book/Tests/Resources/dracula.txt");
            Assert.AreEqual(ext, ".txt");
        }

        [Test]
        public void LoadingPdfFilesAsJpegs() {
            PdfConversion.toJpegs("Assets/Modules/Book/Tests/Resources/atari.pdf", "Assets/Modules/Book/Tests/Output/dracula");
        }
        
        [Test]
        public void LoadingPdfFilesAsSvgs() {
            PdfConversion.toSvgs("Assets/Modules/Book/Tests/Resources/atari.pdf", "Assets/Modules/Book/Tests/Output/atari");
        }
    }

}