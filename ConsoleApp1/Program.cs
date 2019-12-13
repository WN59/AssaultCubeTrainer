using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        // base address and offset

        public static int Base = 0x004E4DBC;
        public static int Health = 0xF4;
        public static int Armor = 0xF8;

        // different base address for player ammo

        public static int PlayerBase = 0x004DF73C;
 
      
       


        static void Main(string[] args)
        {
            // using VAMemory.dll to read memory from the game

            VAMemory vam = new VAMemory("ac_client");

                // declaring some int to read value of pointer we put before the program

                int LocalPlayer1 = vam.ReadInt32((IntPtr)Base);
             
                // array for the different offset for Ammo
                var AmmoOffset = new int[] {  0x378, 0x14, 0x0 };


            while (true)
                {

                    // Changing the health of player

                    int HealthAddress = LocalPlayer1 + Health;
                    vam.WriteInt32((IntPtr)HealthAddress, 9999);


                    // changing the armor of player

                    int ArmorAddress = LocalPlayer1 + Armor;
                    vam.WriteInt32((IntPtr)ArmorAddress, 9999);

               

                // loop for add offset to the playerbase addres 

                 IntPtr pointer = IntPtr.Add((IntPtr)vam.ReadInt32((IntPtr)PlayerBase), AmmoOffset[0]);
                for (int i = 1; i < AmmoOffset.Length; i++)
                {
                    pointer = IntPtr.Add((IntPtr)vam.ReadInt32(pointer), AmmoOffset[i]);
                    
                }

                vam.WriteInt32((IntPtr)pointer, 9999);


                Console.WriteLine(" value: " + vam.ReadInt32(pointer));


                Thread.Sleep(500);


                }
            
           

        }
    }
}
