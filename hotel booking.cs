// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;

class Program
{
    static int[] rooms = new int[10];
    public static string[,] userInfo = new string[10, 3];
    public static int[] days = new int[10];
    static string[] roomTypes = new string[10] { "Luxury", "Luxury", "Double", "Masterbed", "Masterbed", "Suite", "Single", "Double", "Suite", "Single" };
    static int[] roomPrices = new int[10] { 1000, 1000, 500, 700, 700, 600, 300, 500, 600, 300 };

    static void Main(string[] args)
    {
        Console.WriteLine("========== Welcome To Our Hotel ===========");
        bool choosing = true;
        while (choosing)
        {
            Console.WriteLine("\n==============(Hotel Booking)  ==============\n");
            Console.WriteLine("1. View all rooms.   2. Book a room.   ");
            Console.WriteLine("3. Cancel a booking.   4. Check out.  ");
            Console.WriteLine("5. Exit. ");
            Console.Write("\nChoose an option (1 - 5) : ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewRooms();
                    break;
                case "2":
                    BookRooms();
                    break;
                case "3":
                    CancelBooking();
                    break;
                case "4":
                    CheckOut();
                    break;
                case "5":
                    choosing = false;
                    Console.WriteLine("Exiting the system.......");
                    break;
                default:
                    Console.WriteLine("Invalid Choice. Please select an option from 1 to 5.");
                    break;
            }
        }
    }

    static int DayCount()
    {
        while (true)
        {
            Console.Write("How many days would you like to book: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int numDays) && numDays > 0)
            {
                Console.WriteLine($"You have booked {numDays} days");
                return numDays;
            }
            else
            {
                Console.WriteLine("Please enter a valid number");
            }
        }
    }

    static void ViewRooms()
    {
        Console.WriteLine("Room Status: ");
        for (int i = 0; i < rooms.Length; i++)
        {
            Console.WriteLine($"Room {i + 1} ({roomTypes[i]}): {(rooms[i] == 0 ? "Available" : "Booked by " + userInfo[i, 0] + " for " + days[i] + " days")}");
        }
    }

    static void BookRooms()
    {
        Console.WriteLine("\nLuxury : 1000THB  Masterbed : 700THB\n" + "Suite : 600THB   Double :  500THB   Single : 300THB");
        int roomNo;
        while (true)
        {
            Console.Write("\nEnter the room number to book (1 - 10): ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out roomNo) && roomNo >= 1 && roomNo <= 10)
            {
                roomNo--; // Adjust for zero-based index
                break; // Exit the loop if input is valid
            }
            else
            {
                Console.WriteLine("Invalid Room Number. Please select a room from 1 to 10 to book.");
            }
        }
        if (rooms[roomNo] == 1)
        {
            Console.WriteLine("Room is already booked.");
        }
        else
        {
            rooms[roomNo] = 1;
            CustomerInfo(roomNo);
            days[roomNo] = DayCount();
            Console.WriteLine($"Room {roomNo + 1} has been successfully booked.");
        }
    }

    static void CustomerInfo(int i)
    {
        Console.WriteLine("Please enter your name, Passport and Password.");
        Console.Write("Name: ");
        userInfo[i, 0] = Console.ReadLine();
        Console.Write("Passport: ");
        userInfo[i, 1] = Console.ReadLine();
        Console.Write("Password: ");
        userInfo[i, 2] = Console.ReadLine();
    }

    static void CancelBooking()
    {
        Console.WriteLine("Booked Rooms:");
        bool hasBooking = false;
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i] == 1)
            {
                Console.WriteLine($"Room ({i + 1}) is booked by {userInfo[i, 0]}");
                hasBooking = true;
            }
        }
        if (!hasBooking)
        {
            Console.WriteLine("No rooms are currently booked.");
            return;
        }
        int roomNo;
        while (true)
        {
            Console.Write("Enter the room number to cancel: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out roomNo) && roomNo >= 1 && roomNo <= 10)
            {
                roomNo--; // Adjust for zero-based index
                break;
            }
            else
            {
                Console.WriteLine("Invalid Room Number. Please select a room from 1 to 10 to cancel.");
            }
        }
        if (rooms[roomNo] == 0)
        {
            Console.WriteLine("Room is already available.");
        }
        else
        {
            Console.Write("Please enter your Password: ");
            int attempts = 0;
            while (true)
            {
                string password = Console.ReadLine();
                if (password == userInfo[roomNo, 2])
                {
                    rooms[roomNo] = 0;
                    Console.WriteLine($"Room {roomNo + 1} has been successfully canceled.");
                    break;
                }
                else
                {
                    attempts++;
                    if (attempts >= 5)
                    {
                        Console.WriteLine("Maximum attempts reached. Exiting cancellation process.");
                        break;
                    }
                    Console.Write("Incorrect password! Please enter your password again: ");
                }
            }
        }
    }

    static int FeeCalculate(int roomNo)
    {
        int Price = roomPrices[roomNo] * days[roomNo];
        return Price;
    }

    static void CheckOut()
    {
        Console.Write("Enter the room number to checkout: ");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int roomNo) || roomNo < 1 || roomNo > 10)
        {
            Console.WriteLine("Invalid Room Number. Please select a room from 1 to 10 to checkout.");
            return; // Exit the method if input is invalid
        }
        roomNo--;
        if (rooms[roomNo] == 0)
        {
            Console.WriteLine("This Room is not booked!!!!!");
        }
        else
        {
            rooms[roomNo] = 0;
            Console.WriteLine("The total price is {0} THB", FeeCalculate(roomNo));
            Console.WriteLine($"Room {roomNo + 1} has been successfully checked out.");
        }
    }

}
