using System;

namespace Hi_Tech_Management_System.Business
{
    [Serializable]
    public class Book : Author
    {
        private long bookISBN;
        private string bookTitle;
        private decimal bookPrice;
        private string bookPublishedDate;
        private int bookQOH;
        private string bookCategory;
        private string bookPublisher;
        private int bookOrderQuantity;
        private string bookImgPath;
        private string itemType;
        private string webLink;

        public Book() : base()
        {
            this.bookISBN = 0;
            this.bookTitle = "Default";
            this.bookPrice = 0;
            this.bookPublishedDate = "10/10/2010";
            this.bookQOH = 0;
            this.bookCategory = "Default";
            this.bookPublisher = "Default";
            this.bookOrderQuantity = 0;
        }

        public Book(long bookISBN, string bookTitle, decimal bookPrice, string bookPublishedDate, int bookQOH, string bookCategory, string bookPublisher, int bookOrderQuantity, string bookImgPath, string itemType, string webLink)
        {
            this.bookISBN = bookISBN;
            this.bookTitle = bookTitle;
            this.bookPrice = bookPrice;
            this.bookPublishedDate = bookPublishedDate;
            this.bookQOH = bookQOH;
            this.bookCategory = bookCategory;
            this.bookPublisher = bookPublisher;
            this.bookOrderQuantity = bookOrderQuantity;
            this.bookImgPath = bookImgPath;
            this.itemType = itemType;
            this.webLink = webLink;
        }

        public long BookISBN
        {
            get
            {
                return bookISBN;
            }

            set
            {
                bookISBN = value;
            }
        }

        public string BookTitle
        {
            get
            {
                return bookTitle;
            }

            set
            {
                bookTitle = value;
            }
        }

        public decimal BookPrice
        {
            get
            {
                return bookPrice;
            }

            set
            {
                bookPrice = value;
            }
        }

        public string BookPublishedDate
        {
            get
            {
                return bookPublishedDate;
            }

            set
            {
                bookPublishedDate = value;
            }
        }

        public int BookQOH
        {
            get
            {
                return bookQOH;
            }

            set
            {
                bookQOH = value;
            }
        }

        public string BookCategory
        {
            get
            {
                return bookCategory;
            }

            set
            {
                bookCategory = value;
            }
        }

        public string BookPublisher
        {
            get
            {
                return bookPublisher;
            }

            set
            {
                bookPublisher = value;
            }
        }

        public int BookOrderQuantity
        {
            get
            {
                return bookOrderQuantity;
            }

            set
            {
                bookOrderQuantity = value;
            }
        }

        public string BookImgPath
        {
            get
            {
                return bookImgPath;
            }

            set
            {
                bookImgPath = value;
            }
        }

        public string ItemType
        {
            get
            {
                return itemType;
            }

            set
            {
                itemType = value;
            }
        }

        public string WebLink
        {
            get
            {
                return webLink;
            }

            set
            {
                webLink = value;
            }
        }
    }
}
