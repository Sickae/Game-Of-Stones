using System;
using System.Linq;

namespace GameOfStones
{
    class Program
    {
        static void Main(string[] args)
        {
            int t = Convert.ToInt32(Console.ReadLine());
            for(int i = 0; i < t; i++)
            {
                int n = Convert.ToInt32(Console.ReadLine());
                game(n);
            }
        }

        static void game(int n)
        {
            Game game = new Game(n);
            int optimal = Enum.GetValues(typeof(Moves))
                .Cast<Int32>()
                .Min() + Enum.GetValues(typeof(Moves))
                .Cast<Int32>()
                .Max();
            while(game.p.All(i => !i.Lost))
            {
                for(int i = 0; i < 2; i++)
                {
                    if (game.Stones >= Enum.GetValues(typeof(Moves)).Cast<Int32>().Min())
                    {
                        if (Enum.GetValues(typeof(Moves))
                            .Cast<Int32>()
                            .Any(x => x == game.Stones))
                            game.RemoveStones(Enum.GetValues(typeof(Moves))
                                .Cast<Int32>()
                                .Single(x => x == game.Stones));
                        else
                            game.RemoveStones(Enum.GetValues(typeof(Moves))
                                .Cast<Int32>()
                                .LastOrDefault(x => (game.Stones - x) % optimal <= 1));
                    }
                    else
                    {
                        game.p[i].Lost = true;
                        break;
                    }
                }
            }
            Console.WriteLine(game.p[1].Lost ? "First" : "Second");
        }
    }
    
    enum Moves
    {
        M1 = 2,
        M2 = 3,
        M3 = 5
    }

    class Game
    {
        int stones { get; set; }
        public Player[] p = { new Player(), new Player() };

        public Game(int stones)
        {
            if (stones >= Enum.GetValues(typeof(Moves)).Cast<Int32>().Min())
                Stones = stones;
            else p[0].Lost = true;
        }

        public int Stones
        {
            get { return stones; }
            set { stones = value; }
        }

        public void RemoveStones(int n)
        {
            if (n > 0) stones -= n;
            else RemoveStones(Enum.GetValues(typeof(Moves))
                .Cast<Int32>()
                .Where(x => x <= stones && !Enum.IsDefined(typeof(Moves), stones - x))
                .Max());
        }
    }

    class Player
    {
        bool lost { get; set; } = false;

        public bool Lost
        {
            get { return lost; }
            set { lost = value; }
        }
    }
}
