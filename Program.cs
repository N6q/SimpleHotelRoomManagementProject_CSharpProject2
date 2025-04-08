using System;
namespace SimpleHotelRoomManagementProject_CSharpProject2
{
    internal class Program
    {
        // Declaring Arrays
        static int MAX_ROOMS = 100;
        static int[] roomNumbers = new int[MAX_ROOMS];
        static double[] roomRates = new double[MAX_ROOMS];
        static bool[] isReserved = new bool[MAX_ROOMS];
        static string[] guestNames = new string[MAX_ROOMS];
        static int[] nights = new int[MAX_ROOMS];
        static DateTime[] bookingDates = new DateTime[MAX_ROOMS];
        static int roomCount = 0;
      

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                // Menu Options
                Console.WriteLine("\nChoose an Array Exercise:");
                Console.WriteLine("1. Add a new room  (Room Number, Daily Rate)  ");
                Console.WriteLine("2. View all rooms (Available and Reserved) ");
                Console.WriteLine("3. Reserve a room for a guest (Guest Name, Room Number, Nights) ");
                Console.WriteLine("4. View all reservations with total cost ");
                Console.WriteLine("5. Search reservation by guest name (case-insensitive) ");
                Console.WriteLine("6. Find the highest-paying guest  ");
                Console.WriteLine("7. Cancel a reservation by room number  ");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1: AddNewRoom(); break;
                        case 2: ViewAllRooms(); break;
                        case 3: ReserveRoom(); break;
                        case 4: ViewAllReservations(); break;
                        case 5: SearchReservationByGuestName(); break;
                        case 6: highestPayingGuest(); break;
                        case 7: CancelReservation(); break;
                        case 8: return;
                        default: Console.WriteLine("Invalid choice! Try again."); break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter a number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                Console.ReadLine();
            }
        }

        /* -------------------------- 1. Add New Room  --------------------------*/
        static void AddNewRoom()
        {
            try
            {
                int RoomNum;
                double DailyRate;
                

                // Check if the array is full

                if (roomCount == MAX_ROOMS)
                {
                    Console.WriteLine("Array is Full! You can't add more Rooms.");
                }
                else
                {
                    Console.WriteLine("\n<--------------- Enter Room Information --------------->");

                    // Get and validate Room Number
                        Console.Write("Enter Room Number: ");
                        RoomNum = int.Parse(Console.ReadLine());



                    // Check if room number already exists
                    for (int j = 0; j < roomCount; j++)
                    {
                        while (roomNumbers[j] == RoomNum)
                        {
                            Console.WriteLine("Room Number Already Taken! Try another.");
                            RoomNum = int.Parse(Console.ReadLine());
                        }
                    }   
                    roomNumbers[roomCount] = RoomNum;

                    // Get and validate Daily Rate
                    Console.Write("Enter Room Daily Rate: ");
                    DailyRate = double.Parse(Console.ReadLine());

                    while (DailyRate < 100)
                    {
                        Console.Write("Invalid Rate! Rate should be 100 or more ");
                        DailyRate = double.Parse(Console.ReadLine());
                    }

                    roomRates[roomCount] = DailyRate;


                    // Make Reservation Status available by default
                   
                    isReserved[roomCount] = false;

                    Console.WriteLine($"Room Number: {roomNumbers[roomCount]} , Daily Rate: {roomRates[roomCount]} , Reserved: {isReserved[roomCount]}");
                    roomCount++;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter numbers where required.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("(------ Press Enter To Go Back To MENU ------)");
            Console.ReadLine();
        }





        static void ViewAllRooms()
        { 
            for (int i = 0; i < roomCount; i++)
            {
                // Displaying Room Information
                if (isReserved[i] == true)
                {
                    double total = (nights[i] * roomRates[i]);
                    Console.WriteLine($"{i}: Number Of Room: {roomNumbers[i]} , Room Daliy rate: {roomRates[i]} , Room Reservestion: Reserved , By: {guestNames[i]} , Total Cost: {total} \n");
                }
                else
                {
                    Console.WriteLine($"{i}: Number Of Room: {roomNumbers[i]} , Room Daliy rate: {roomRates[i]} , Room Reservestion: Available  \n");
                }

            }

        }



        static void ReserveRoom()
        { 
            if (roomCount == 0)
            {
                Console.WriteLine("No Rooms Available! Please add a room first.");
                return;
            }
            Console.WriteLine("\n<--------------- Enter Reservation Information --------------->");

            Console.Write("Enter Guest Name: ");
            string guestName = Console.ReadLine();

            Console.Write("Enter Room Number: ");
            int roomNum = int.Parse(Console.ReadLine());





            Console.Write("Enter Number of Nights: ");
            int NumNights = int.Parse(Console.ReadLine());

            while (NumNights < 1)
            {
                Console.Write("Invalid Number of Nights! Please enter a valid number: ");
                NumNights = int.Parse(Console.ReadLine());
            }

            DateTime bookingDate = DateTime.Now;
            
            
            
            // Check if the room number exists
            bool roomExists = false;
            for (int i = 0; i < roomCount; i++)
            {
                if (roomNumbers[i] == roomNum)
                {
                    roomExists = true;
                    // Check if the room is already reserved
                    if (isReserved[i])
                    {
                        Console.WriteLine("Room is already reserved! Please choose another room.");
                        return;
                    }
                    else
                    {
                        // Reserve the room
                        isReserved[i] = true;
                        guestNames[i] = guestName;
                        nights[i] = NumNights;
                        bookingDates[i] = bookingDate;
                        Console.WriteLine($"Room {roomNum} has been reserved for {guestName} for {NumNights} nights.");
                    }
                }
            }
            if (!roomExists)
            {
                Console.WriteLine("Room number does not exist! Please check the room number.");
            }
            else
            {
                // Displaying Reservation Information
                for (int i = 0; i < roomCount; i++)
                {
                    if (roomNumbers[i] == roomNum)
                    {
                        double total = (nights[i] * roomRates[i]);
                        Console.WriteLine($"Guest Name: {guestNames[i]} , Room Number: {roomNumbers[i]} , Room Daliy rate: {roomRates[i]} , Total Cost: {total}   \n");
                    }
                }
            }
            Console.WriteLine("(------ Press Enter To Go Back To MENU ------)");
        }



        static void ViewAllReservations()
        {
            for (int i =0; i < roomCount; i++)
            {
                if(!isReserved[i] == true)
                {
                    double total = (nights[i] * roomRates[i]);
                    Console.WriteLine($"Guest Name: {guestNames[i]} , Room Number: {roomNumbers[i]} , Room Daliy rate: {roomRates[i]} , Total Cost: {total}   \n");
                }
            }
        }



        static void SearchReservationByGuestName()
        { }



        static void highestPayingGuest()
        { }



        static void CancelReservation()
        { }



    


    }
    }
