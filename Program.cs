using System;

namespace SimpleHotelRoomManagementProject_CSharpProject2
{
    internal class Program
    {
        // Declaring variables and arrays to manage rooms and reservations
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
            // Main menu loop
            while (true)
            {
                Console.Clear();
                // Display menu options
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
                    // Get user choice
                    int choice = int.Parse(Console.ReadLine());

                    // Handle user choice
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
                    // Handle invalid input
                    Console.WriteLine("Invalid input format. Please enter a number.");
                }
                catch (Exception ex)
                {
                    // Handle unexpected errors
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                Console.ReadLine();
            }
        }

        /* -------------------------- 1. Add New Room  --------------------------*/
        static void AddNewRoom()
        {
            Console.Clear();
            try
            {
                // Check if the array is full
                if (roomCount == MAX_ROOMS)
                {
                    Console.WriteLine("Array is Full! You can't add more Rooms.");
                }
                else
                {
                    // Ask user to enter room information
                    Console.WriteLine("\n<--------------- Enter Room Information --------------->");
                    Console.Write("Enter Room Number: ");
                    int RoomNum = int.Parse(Console.ReadLine());

                    // Check if room number already exists
                    for (int j = 0; j < roomCount; j++)
                    {
                        while (roomNumbers[j] == RoomNum)
                        {
                            Console.WriteLine("Room Number Already Taken! Try another. \n");
                            RoomNum = int.Parse(Console.ReadLine());
                        }
                    }
                    roomNumbers[roomCount] = RoomNum;

                    // Ask user to enter daily rate
                    Console.Write("Enter Room Daily Rate: ");
                    double DailyRate = double.Parse(Console.ReadLine());

                    // Validate daily rate
                    while (DailyRate < 100)
                    {
                        Console.Write("Invalid Rate! Rate should be 100 or more \n Enter Rate again: \n");
                        DailyRate = double.Parse(Console.ReadLine());
                    }
                    roomRates[roomCount] = DailyRate;

                    // Mark room as available
                    isReserved[roomCount] = false;

                    // Display added room details
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

        /* -------------------------- 2. View all Rooms  --------------------------*/
        static void ViewAllRooms()
        {
            Console.Clear();
            try
            {
                // Display all rooms
                for (int i = 0; i < roomCount; i++)
                {
                    if (isReserved[i])
                    {
                        // Display reserved room details
                        double total = (nights[i] * roomRates[i]);
                        Console.WriteLine($"{i}: Room Number: {roomNumbers[i]} , Daily Rate: {roomRates[i]} , Reserved By: {guestNames[i]} , Total Cost: {total} \n");
                    }
                    else
                    {
                        // Display available room details
                        Console.WriteLine($"{i}: Room Number: {roomNumbers[i]} , Daily Rate: {roomRates[i]} , Status: Available  \n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while viewing rooms: {ex.Message}");
            }

            Console.WriteLine("(------ Press Enter To Go Back To MENU ------)");
            Console.ReadLine();
        }

        /* -------------------------- 3. Reserve Room  --------------------------*/
        static void ReserveRoom()
        {
            Console.Clear();
            try
            {
                // Check if there are any rooms available
                if (roomCount == 0)
                {
                    Console.WriteLine("No Rooms Available! Please add a room first.");
                    return;
                }

                // Ask user to enter reservation details
                Console.WriteLine("\n<--------------- Enter Reservation Information --------------->");
                Console.Write("Enter Guest Name: ");
                string guestName = Console.ReadLine().ToLower();
                Console.Write("Enter Room Number: ");
                int roomNum = int.Parse(Console.ReadLine());
                Console.Write("Enter Number of Nights: ");
                int NumNights = int.Parse(Console.ReadLine());

                // Validate number of nights
                while (NumNights < 1)
                {
                    Console.Write("Invalid Number of Nights! Please enter a valid number: ");
                    NumNights = int.Parse(Console.ReadLine());
                }

                DateTime bookingDate = DateTime.Now;

                // Check if the room exists and reserve it
                bool roomExists = false;
                for (int i = 0; i < roomCount; i++)
                {
                    if (roomNumbers[i] == roomNum)
                    {
                        roomExists = true;
                        if (isReserved[i])
                        {
                            Console.WriteLine("Room is already reserved! Please choose another room.");
                            return;
                        }
                        else
                        {
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
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter numbers where required.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reserving the room: {ex.Message}");
            }

            Console.WriteLine("(------ Press Enter To Go Back To MENU ------)");
            Console.ReadLine();
        }

        /* -------------------------- 4. View All Reservations  --------------------------*/
        static void ViewAllReservations()
        {
            Console.Clear();
            try
            {
                // Display all reservations
                for (int i = 0; i < roomCount; i++)
                {
                    if (isReserved[i])
                    {
                        double total = (nights[i] * roomRates[i]);
                        Console.WriteLine($"Guest Name: {guestNames[i]} , Room Number: {roomNumbers[i]} , Daily Rate: {roomRates[i]} , Total Cost: {total}   \n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while viewing reservations: {ex.Message}");
            }

            Console.WriteLine("(------ Press Enter To Go Back To MENU ------)");
            Console.ReadLine();
        }

        /* -------------------------- 5. Search Reservation By Guest Name  --------------------------*/
        static void SearchReservationByGuestName()
        {
            Console.Clear();
            try
            {
                // Check if there are any rooms available
                if (roomCount == 0)
                {
                    Console.WriteLine("No Rooms Available! Please add a room first.");
                    return;
                }

                // Ask user to enter guest name
                Console.WriteLine("Enter Guest Name to Search: ");
                string searchName = Console.ReadLine().ToLower();
                bool found = false;

                // Search for reservations by guest name
                for (int i = 0; i < roomCount; i++)
                {
                    if (guestNames[i]?.ToLower() == searchName)
                    {
                        found = true;
                        double total = (nights[i] * roomRates[i]);
                        Console.WriteLine($"Guest Name: {guestNames[i]} , Room Number: {roomNumbers[i]} , Daily Rate: {roomRates[i]} , Total Cost: {total}   \n");
                    }
                }

                if (!found)
                {
                    Console.WriteLine("No reservation found for the given guest name.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching for reservations: {ex.Message}");
            }

            Console.WriteLine("(------ Press Enter To Go Back To MENU ------)");
            Console.ReadLine();
        }

        /* -------------------------- 6. Find Highest Paying Guest  --------------------------*/
        static void highestPayingGuest()
        {
            Console.Clear();
            try
            {
                // Check if there are any rooms available
                if (roomCount == 0)
                {
                    Console.WriteLine("No Rooms Available! Please add a room first.");
                    return;
                }

                // Find the highest-paying guest
                double maxCost = 0;
                string highestGuest = "";

                for (int i = 0; i < roomCount; i++)
                {
                    if (isReserved[i])
                    {
                        double total = (nights[i] * roomRates[i]);
                        if (total > maxCost)
                        {
                            maxCost = total;
                            highestGuest = guestNames[i];
                        }
                    }
                }

                if (string.IsNullOrEmpty(highestGuest))
                {
                    Console.WriteLine("No reservations found.");
                }
                else
                {
                    Console.WriteLine($"Highest Paying Guest: {highestGuest} with a total cost of {maxCost}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while finding the highest paying guest: {ex.Message}");
            }

            Console.WriteLine("(------ Press Enter To Go Back To MENU ------)");
            Console.ReadLine();
        }

        /* -------------------------- 7. Cancel a Reservation  --------------------------*/
        static void CancelReservation()
        {
            Console.Clear();
            try
            {
                // Check if there are any rooms available
                if (roomCount == 0)
                {
                    Console.WriteLine("No Rooms Available! Please add a room first.");
                    return;
                }

                // Ask user to enter room number to cancel
                Console.Write("Enter Room Number to Cancel Reservation: ");
                int roomNum = int.Parse(Console.ReadLine());
                bool roomFound = false;

                // Cancel the reservation
                for (int i = 0; i < roomCount; i++)
                {
                    if (roomNumbers[i] == roomNum)
                    {
                        roomFound = true;
                        if (isReserved[i])
                        {
                            isReserved[i] = false;
                            guestNames[i] = null;
                            nights[i] = 0;
                            bookingDates[i] = DateTime.MinValue;
                            Console.WriteLine($"Reservation for Room {roomNum} has been canceled.");
                        }
                        else
                        {
                            Console.WriteLine("Room is not reserved! No reservation to cancel.");
                        }
                    }
                }

                if (!roomFound)
                {
                    Console.WriteLine("Room number not found! Please check the room number.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid room number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while canceling the reservation: {ex.Message}");
            }

            Console.WriteLine("(------ Press Enter To Go Back To MENU ------)");
            Console.ReadLine();
        }
    }
}
