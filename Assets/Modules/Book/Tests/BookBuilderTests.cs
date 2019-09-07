using System;
using NUnit.Framework;

namespace Modules.Book.Tests {
    
    public class BookBuilderTests {
        
        [Test]
        public void buildingBasicBook() {

            BookMetaInfo bookMetaInfo = new BookMetaInfo();
            bookMetaInfo.title = "Dracula";
            bookMetaInfo.author = "Bram Stoker";
            bookMetaInfo.publisher = "Archibald Constable and Company (UK)";
            bookMetaInfo.pageCount = 368;
            bookMetaInfo.language = "English";
            bookMetaInfo.description = "Dracula is an 1897 Gothic horror novel by Irish author Bram Stoker";
            bookMetaInfo.publicationDate = new DateTime(1987, 5, 26);
            bookMetaInfo.category = "Gothic horror";
            bookMetaInfo.tags = new[] {"gothic", "horror", "vampires", "classic"};
            BasicBook book = new BasicBook("Assets/Modules/Book/Tests/Resources/dracula.txt");
            Assert.AreEqual(typeof(BasicBook), book.GetType());
        }
    }
}