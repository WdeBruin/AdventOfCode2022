namespace Advent.Solutions;

public class Day09 : DayBase
{
    public override void Run()
    {
        var lines = ReadInput("Day09.txt");

        var solution = Solve1(lines);

        WriteLine($"Answer part 1: {solution}");

        var solution2 = Solve2(lines);
        WriteLine($"Answer part 2: {solution2}");
    }

    public string Solve2(string[] lines)
    {
        return "";
    }

    public string Solve1(string[] lines)
    {
        var game = new Game();
        foreach (var line in lines)
        {
            game.ProcessMoveLine(line);
        }

        return game.Board.VisitedCount.ToString();
    }

    class Game
    {
        public Board Board { get; set; }

        public Game()
        {
            Board = new Board();
        }

        public void ProcessMoveLine(string line)
        {
            var dir = line.Split(' ')[0];
            var n = int.Parse(line.Split(' ')[1]);

            for (int i = 0; i < n; i++)
            {
                Board.Move(dir);
            }
        }        
    }

    class Board
    {
        List<Field> Fields;

        Field HeadField => Fields.SingleOrDefault(x => x.HasHead);
        Field TailField => Fields.SingleOrDefault(x => x.HasTail);
        Field StartField => Fields.SingleOrDefault(x => x.IsStart);

        public int VisitedCount
        {
            get
            {
                return Fields.Where(x => x.IsVisited).Count();
            }
        }

        public Board()
        {
            Fields = new List<Field>
            {
                new Field
                {
                    Pos = (0,0),
                    IsStart = true,
                    HasHead = true,
                    HasTail = true,
                }
            };
        }

        public void Move(string direction)
        {
            MoveHead(direction);
            MoveTail();
        }

        private void MoveHead(string direction)
        {
            // Move head in direction
            (int x, int y) targetPos = direction switch
            {
                "R" => (HeadField.Pos.x + 1, HeadField.Pos.y),
                "L" => (HeadField.Pos.x - 1, HeadField.Pos.y),
                "U" => (HeadField.Pos.x, HeadField.Pos.y - 1),
                "D" => (HeadField.Pos.x, HeadField.Pos.y + 1),
                _ => throw new NotImplementedException()
            } ;
            var targetField = GetOrCreateField(targetPos);
            HeadField.HasHead = false;
            targetField.HasHead = true;
        }

        private void MoveTail()
        {
            (int x, int y) targetPos = TailField.Pos;

            if (TailField.Pos.x == HeadField.Pos.x - 2)
            {
                if (TailField.Pos.y == HeadField.Pos.y)
                {
                    targetPos = (TailField.Pos.x + 1, TailField.Pos.y);
                }
                else if (TailField.Pos.y == HeadField.Pos.y + 1)
                {
                    targetPos = (TailField.Pos.x + 1, TailField.Pos.y - 1);
                }
                else if (TailField.Pos.y == HeadField.Pos.y - 1)
                {
                    targetPos = (TailField.Pos.x + 1, TailField.Pos.y + 1);
                }
            }
            else if (TailField.Pos.x == HeadField.Pos.x + 2)
            {
                if (TailField.Pos.y == HeadField.Pos.y)
                {
                    targetPos = (TailField.Pos.x - 1, TailField.Pos.y);
                }
                else if (TailField.Pos.y == HeadField.Pos.y + 1)
                {
                    targetPos = (TailField.Pos.x - 1, TailField.Pos.y - 1);
                }
                else if (TailField.Pos.y == HeadField.Pos.y - 1)
                {
                    targetPos = (TailField.Pos.x - 1, TailField.Pos.y + 1);
                }
            }
            else if (TailField.Pos.y == HeadField.Pos.y + 2)
            {
                if (TailField.Pos.x == HeadField.Pos.x)
                {
                    targetPos = (TailField.Pos.x, TailField.Pos.y - 1);
                }
                else if (TailField.Pos.x == HeadField.Pos.x + 1)
                {
                    targetPos = (TailField.Pos.x - 1, TailField.Pos.y - 1);
                }
                else if (TailField.Pos.x == HeadField.Pos.x - 1)
                {
                    targetPos = (TailField.Pos.x + 1, TailField.Pos.y - 1);
                }
            }
            else if (TailField.Pos.y == HeadField.Pos.y - 2)
            {
                if (TailField.Pos.x == HeadField.Pos.x)
                {
                    targetPos = (TailField.Pos.x, TailField.Pos.y + 1);
                }
                else if (TailField.Pos.x == HeadField.Pos.x + 1)
                {
                    targetPos = (TailField.Pos.x - 1, TailField.Pos.y + 1);
                }
                else if (TailField.Pos.x == HeadField.Pos.x - 1)
                {
                    targetPos = (TailField.Pos.x + 1, TailField.Pos.y + 1);
                }
            }

            if(targetPos != TailField.Pos)
            {
                var targetField = GetOrCreateField(targetPos);
                TailField.HasTail = false;
                targetField.HasTail = true;
            }            
        }

        private Field GetOrCreateField((int x, int y) pos)
        {
            var field = Fields.FirstOrDefault(x => x.Pos == pos);
            if (field == null)
            {
                Fields.Add(new Field
                {
                    Pos = pos
                });
            }

            return Fields.Single(x => x.Pos == pos);
        }
    }

    class Field
    {
        public bool HasHead { get; set; }

        private bool _hasTail;
        public bool HasTail {
            get
            {
                return _hasTail;
            }
            set
            {
                _hasTail = value;

                if (value)
                    IsVisited = true;
            }
        }

        public bool IsStart { get; set; }
        public bool IsVisited { get; private set; }

        public (int x, int y) Pos { get; set; }
    }
}
