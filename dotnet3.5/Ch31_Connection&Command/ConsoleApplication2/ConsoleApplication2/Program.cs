using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Book
    {
        public int BNum
        {
            get;
            private set;
        }
        public string Title
        {
            get;
            private set;
        }
        public string Author
        {
            get;
            private set;
        }
        public string Publisher
        {
            get;
            private set;
        }
        public int Price
        {
            get;
            private set;
        }
        public Book(int bnum, string title, string author, string pub, int price)
        {
            BNum = bnum;
            Title = title;
            Author = author;
            Publisher = pub;
            Price = price;
        }
        public override string ToString()
        {
            return Title;
        }
    }

    class Books
    {
        Book[] books = new Book[100];
        public Book this[int bnum]
        {
            get
            {
                int i = 0;
                for (i = 0; i < books.Length; i++)
                {
                    if (books[i] == null)
                    {
                        break;
                    }
                    if (books[i].BNum == bnum)
                    {
                        return books[i];
                    }
                }
                return null;
            }
            set
            {
                int i = 0;
                for (i = 0; i < books.Length; i++)
                {
                    if ((books[i] == null) || (books[i].BNum == bnum))
                    {
                        break;
                    }
                }
                if (i == books.Length)
                {
                    return;
                }
                books[i] = value;
            }
        }

        internal void ViewAll()
        {
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i] == null)
                {
                    break;
                }
                ViewBook(books[i]);
            }
        }

        private void ViewBook(Book book)
        {
            Console.WriteLine("<{0}>", book.BNum);
            Console.WriteLine("\t제목:{0}", book.Title);
            Console.WriteLine("\t출판사:{0}", book.Publisher);
            Console.WriteLine("\t저자:{0}", book.Author);
            Console.WriteLine("\t가격:{0}", book.Price);
        }
    }
    internal class BookManager
    {
        Books books = new Books();
        internal void Run()
        {
            ConsoleKey key = ConsoleKey.NoName;
            while ((key = SelectMenu()) != ConsoleKey.Escape)//반복(메뉴 선택 한 것이 ESC가 아니라면)
            {
                switch (key)//선택한 메뉴에 따라
                {
                    case ConsoleKey.F1: Insert(); break;//F1이면 추가
                    case ConsoleKey.F2: Delete(); break;//F2이면 삭제
                    case ConsoleKey.F3: Search(); break;//F3이면 조회
                    case ConsoleKey.F4: books.ViewAll(); break;//F4이면 전체 보기
                    default: Console.WriteLine("잘못 선택하였습니다."); break;
                }
                Console.WriteLine("아무키나 누르세요.");
                Console.ReadKey(true);
            }

        }



        private void ViewBook(Book book)
        {
            Console.WriteLine("<{0}>", book.BNum);
            Console.WriteLine("\t제목:{0}", book.Title);
            Console.WriteLine("\t출판사:{0}", book.Publisher);
            Console.WriteLine("\t저자:{0}", book.Author);
            Console.WriteLine("\t가격:{0}", book.Price);
        }

        private void Search()
        {
            //조회할 번호 입력
            int num = 0;
            Console.WriteLine("조회할 도서 번호를 입력:");
            num = int.Parse(Console.ReadLine());

            if (books[num] == null)
            {
                Console.WriteLine("{0}번: 입력하지 않음", num);
            }
            else
            {
                ViewBook(books[num]);
            }
        }

        private void Delete()
        {
            //삭제할 번호 입력
            int num = 0;
            Console.WriteLine("삭제할 도서 번호를 입력:");
            num = int.Parse(Console.ReadLine());

            if (books[num] != null)
            {
                books[num] = null;
            }
        }

        private void Insert()
        {
            //추가할 번호 입력
            int num = 0;
            Console.WriteLine("추가할 도서 번호를 입력:");
            num = int.Parse(Console.ReadLine());

            if (books[num] != null)
            {
                Console.WriteLine("이미 존재합니다.");
                return;
            }
            string title;
            Console.WriteLine("제목");
            title = Console.ReadLine();
            string author;
            Console.WriteLine("저자");
            author = Console.ReadLine();
            string publisher;
            Console.WriteLine("출판사");
            publisher = Console.ReadLine();
            int price = 0;
            Console.WriteLine("가격");
            int.TryParse(Console.ReadLine(), out price);
            books[num] = new Book(num, title, author, publisher, price);
        }

        private ConsoleKey SelectMenu()
        {
            Console.Clear();
            Console.WriteLine("도서 관리 프로그램(인덱서 실습) 메뉴");
            Console.WriteLine("F1: 도서 추가 F2:도서 삭제 F3:도서 조회 F4:전체 보기");
            Console.WriteLine("ESC: 프로그램 종료");
            return Console.ReadKey(true).Key;
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            BookManager bm = new BookManager();
            bm.Run();
        }
    }
}
