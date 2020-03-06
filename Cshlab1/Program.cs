using System;

namespace Cshlab1
{
    class Program
    {
        static int RandomCard(int[] cards, Random rnd)
        {
            while (true)
            {
                int p = rnd.Next(9);
                if (cards[p] == 0) continue;
                cards[p]--;
                return p;
            }
        }
        static void TakeCard(int[] sum, int[] aces, int i, int x, bool cheat)
        {
            switch (x)
            {
                case 0:
                    if (i == 0) Console.Write("You take 6");
                    else if (cheat) Console.Write("Player {0} take 6", i);
                    sum[i] += 6;
                    break;
                case 1:
                    if (i == 0) Console.Write("You take 7");
                    else if (cheat) Console.Write("Player {0} take 7", i);
                    sum[i] += 7;
                    break;
                case 2:
                    if (i == 0) Console.Write("You take 8");
                    else if (cheat) Console.Write("Player {0} take 8", i);
                    sum[i] += 8;
                    break;
                case 3:
                    if (i == 0) Console.Write("You take 9");
                    else if (cheat) Console.Write("Player {0} take 9", i);
                    sum[i] += 9;
                    break;
                case 4:
                    if (i == 0) Console.Write("You take 10");
                    else if (cheat) Console.Write("Player {0} take 10", i);
                    sum[i] += 10;
                    break;
                case 5:
                    if (i == 0) Console.Write("You take jack");
                    else if (cheat) Console.Write("Player {0} take jack", i);
                    sum[i] += 2;
                    break;
                case 6:
                    if (i == 0) Console.Write("You take dame");
                    else if (cheat) Console.Write("Player {0} take dame", i);
                    sum[i] += 3;
                    break;
                case 7:
                    if (i == 0) Console.Write("You take king");
                    else if (cheat) Console.Write("Player {0} take king", i);
                    sum[i] += 4;
                    break;
                case 8:
                    if (i == 0) Console.Write("You take ace");
                    else if (cheat) Console.Write("Player {0} take ace", i);
                    sum[i] += 11;
                    aces[i]++;
                    break;
            }
            while (sum[i] > 21 && aces[i] > 0)
            {
                sum[i] -= 10;
                aces[i]--;
            }
            if (cheat) Console.WriteLine(", now sum is {0}", sum[i]);
            else if (i == 0) Console.WriteLine();
        }
        static bool ReadWithCheck()
        {
            char c = '*';
            while (true)
            {
                Console.WriteLine("(Y)es or (N)o");
                c = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (c == 'y' || c == 'Y') return true;
                else
                    if (c == 'n' || c == 'N') return false;
                else
                    Console.WriteLine("error");
            }
        }
        static void Main(string[] args)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            do
            {
                Console.Clear();
                int[] cards = new int[9];
                for (int i = 0; i < 9; i++)
                    cards[i] = 4;
                int[] aces = new int[9];
                for (int i = 0; i < 9; i++)
                    aces[i] = 0;
                Console.WriteLine("Welcome to the club buddy!");
                int pc, k;
                bool cheat;
                char c;
                while (true)
                {
                    Console.WriteLine("Enter amount of players (2 - 8): ");
                    c = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    if (c >= '2' && c <= '8')
                    {
                        pc = c - '0';
                        break;
                    }
                    Console.WriteLine("error");
                }
                //Console.WriteLine("Initial amount of money: "); 
                //smn = Convert.ToInt32(Console.ReadLine()); 
                //  k = pc; 
                Console.WriteLine("Do you want use cheat mode?");
                if (ReadWithCheck())
                {
                    Console.WriteLine(";)");
                    cheat = true;
                }
                else cheat = false;
                k = pc;
                bool[] play = new bool[8];
                for (int i = 0; i < 8; i++)
                    play[i] = true;
                int[] sum = new int[8];
                for (int i = 0; i < 8; i++)
                    sum[i] = 0;
                Console.Clear();
                while (k > 0)
                {
                    int x;
                    for (int i = 1; i < pc; i++)
                        if (play[i])
                        {
                            x = rnd.Next(10) + 1;
                            if (sum[i] + x <= 21)
                            {
                                TakeCard(sum, aces, i, RandomCard(cards, rnd), cheat);
                                if (sum[i] >= 21)
                                {
                                    play[i] = false;
                                    k--;
                                }
                            }
                            else
                            {
                                play[i] = false;
                                k--;
                            }

                        }
                    for (int i = 0; i < pc; i++)
                    {
                        if (i == 0) Console.Write("You ");
                        else
                            Console.Write("Player " + i + " ");
                        if (play[i])
                            Console.WriteLine("still playing");
                        else
                        {
                            Console.Write("is no longer playing");
                            if (cheat) Console.WriteLine(", sum is {0}", sum[i]);
                            else Console.WriteLine();
                        }
                    }
                    if (play[0])
                    {
                        if (cheat)
                        {
                            for (int i = 0; i < 5; i++)
                                Console.WriteLine("Amount of {0}: {1}", i + 6, cards[i]);
                            Console.WriteLine("Amount of jack: {0}", cards[5]);
                            Console.WriteLine("Amount of dame: {0}", cards[6]);
                            Console.WriteLine("Amount of king: {0}", cards[7]);
                            Console.WriteLine("Amount of ace: {0}", cards[8]);
                        }
                        Console.WriteLine("You have sum = {0}", sum[0]);
                        if (aces[0] != 0) Console.WriteLine("You have {0} ace", aces[0]);
                        Console.WriteLine("take another card?");
                        if (ReadWithCheck())
                        {
                            Console.Clear();
                            TakeCard(sum, aces, 0, RandomCard(cards, rnd), cheat);
                            if (sum[0] >= 21)
                            {
                                play[0] = false;
                                Console.WriteLine("Skip to results?");
                                if (!ReadWithCheck()) cheat = true;
                                else cheat = false;
                                Console.Clear();
                                k--;
                            }
                        }
                        else
                        {
                            play[0] = false;
                            Console.WriteLine("Skip to results?");
                            if (!ReadWithCheck()) cheat = true;
                            else cheat = false;
                            Console.Clear();
                            k--;
                        }
                    }
                    else
                    if (cheat)
                    {
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                Console.Clear();
                for (int i = 0; i < pc; i++)
                {
                    if (i == 0) Console.WriteLine("You have sum = {0}", sum[0]);
                    else
                        Console.WriteLine("Player {0} have sum = {1}", i, sum[i]);
                }
                int p = -1;
                for (int i = 0; i < pc; i++)
                    if (sum[i] <= 21)
                        if (p == -1 || sum[i] > sum[p]) p = i;
                Console.WriteLine("So, the result is");
                if (p == -1) Console.WriteLine("No one win :)");
                else
                    for (int i = 0; i < pc; i++)
                        if (sum[i] == sum[p])
                        {
                            if (i == 0) Console.WriteLine("You win!!!");
                            else
                                Console.WriteLine("Player {0} win", i);
                        }
                if (p != -1 && (sum[p] > sum[0] || sum[0] > 21)) Console.WriteLine("Plak plak :'( ");
                Console.WriteLine("Do you want to play again?");
            } while (ReadWithCheck());
            Console.WriteLine("Goodbye me dear ;)");
            Console.ReadKey();
        }


    }
}
