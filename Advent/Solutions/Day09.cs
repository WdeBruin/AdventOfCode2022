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
        var game = new Game2();
        foreach (var line in lines)
        {
            game.ProcessMoveLine(line);
        }

        return game.Board.VisitedCount.ToString();
    }

    public string Solve1(string[] lines)
    {
        var game = new Game1();
        foreach (var line in lines)
        {
            game.ProcessMoveLine(line);
        }

        return game.Board.VisitedCount.ToString();
    }

    class Game1
    {
        public Board1 Board { get; set; }

        public Game1()
        {
            Board = new Board1();
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

    class Board1
    {
        List<Field1> Fields;
        Field1 HeadField => Fields.SingleOrDefault(x => x.HasHead);
        Field1 TailField => Fields.SingleOrDefault(x => x.HasTail);
        Field1 StartField => Fields.SingleOrDefault(x => x.IsStart);

        public int VisitedCount
        {
            get
            {
                return Fields.Where(x => x.IsVisited).Count();
            }
        }

        public Board1()
        {
            Fields = new List<Field1>
            {
                new Field1
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

        private Field1 GetOrCreateField((int x, int y) pos)
        {
            var field = Fields.FirstOrDefault(x => x.Pos == pos);
            if (field == null)
            {
                Fields.Add(new Field1
                {
                    Pos = pos
                });
            }

            return Fields.Single(x => x.Pos == pos);
        }
    }

    class Field1
    {
        public bool HasHead { get; set; }

        private bool _hasTail;
        public bool HasTail {
            get => _hasTail;
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

    class Field2
    {
        public bool HasHead => Pieces.Any(x => x == 0);

        public List<int> Pieces { get; set; } = new List<int>();

        public bool HasTail => Pieces.Any(x => x == 9);

        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }

        public (int x, int y) Pos { get; set; }
    }

    class Board2
    {
        List<Field2> Fields;
        Field2 HeadField => Fields.SingleOrDefault(x => x.HasHead);
        Field2 TailField => Fields.SingleOrDefault(x => x.HasTail);
        Field2 StartField => Fields.SingleOrDefault(x => x.IsStart);

        public int VisitedCount
        {
            get
            {
                return Fields.Where(x => x.IsVisited).Count();
            }
        }

        public Board2()
        {
            Fields = new List<Field2>();

            var field = new Field2
            {
                IsStart = true,
                IsVisited = true,
                Pos = (0, 0)
            };
            for (int i = 0; i <= 9; i++)
            {
                field.Pieces.Add(i);
            }
            Fields.Add(field);
        }

        public void Move(string direction)
        {
            MoveHead(direction);
            MoveTrail();
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
            };
            var targetField = GetOrCreateField(targetPos);
            HeadField.Pieces.Remove(0);
            targetField.Pieces.Add(0);            
        }

        private void MoveTrail()
        {
            for (int piece = 1; piece <= 9; piece++)
            {
                var pieceCurrentField = Fields.Single(x => x.Pieces.Any(x => x == piece));
                (int x, int y) targetPos = pieceCurrentField.Pos;

                var followPieceField = Fields.Single(x => x.Pieces.Any(x => x == piece - 1));

                if (pieceCurrentField.Pos.x == followPieceField.Pos.x - 2)
                {
                    if (pieceCurrentField.Pos.y == followPieceField.Pos.y)
                    {
                        targetPos = (pieceCurrentField.Pos.x + 1, pieceCurrentField.Pos.y);
                    }
                    else if (pieceCurrentField.Pos.y == followPieceField.Pos.y + 1)
                    {
                        targetPos = (pieceCurrentField.Pos.x + 1, pieceCurrentField.Pos.y - 1);
                    }
                    else if (pieceCurrentField.Pos.y == followPieceField.Pos.y - 1)
                    {
                        targetPos = (pieceCurrentField.Pos.x + 1, pieceCurrentField.Pos.y + 1);
                    }
                }
                else if (pieceCurrentField.Pos.x == followPieceField.Pos.x + 2)
                {
                    if (pieceCurrentField.Pos.y == followPieceField.Pos.y)
                    {
                        targetPos = (pieceCurrentField.Pos.x - 1, pieceCurrentField.Pos.y);
                    }
                    else if (pieceCurrentField.Pos.y == followPieceField.Pos.y + 1)
                    {
                        targetPos = (pieceCurrentField.Pos.x - 1, pieceCurrentField.Pos.y - 1);
                    }
                    else if (pieceCurrentField.Pos.y == followPieceField.Pos.y - 1)
                    {
                        targetPos = (pieceCurrentField.Pos.x - 1, pieceCurrentField.Pos.y + 1);
                    }
                }
                else if (pieceCurrentField.Pos.y == followPieceField.Pos.y + 2)
                {
                    if (pieceCurrentField.Pos.x == followPieceField.Pos.x)
                    {
                        targetPos = (pieceCurrentField.Pos.x, pieceCurrentField.Pos.y - 1);
                    }
                    else if (pieceCurrentField.Pos.x == followPieceField.Pos.x + 1)
                    {
                        targetPos = (pieceCurrentField.Pos.x - 1, pieceCurrentField.Pos.y - 1);
                    }
                    else if (pieceCurrentField.Pos.x == followPieceField.Pos.x - 1)
                    {
                        targetPos = (pieceCurrentField.Pos.x + 1, pieceCurrentField.Pos.y - 1);
                    }
                }
                else if (pieceCurrentField.Pos.y == followPieceField.Pos.y - 2)
                {
                    if (pieceCurrentField.Pos.x == followPieceField.Pos.x)
                    {
                        targetPos = (pieceCurrentField.Pos.x, pieceCurrentField.Pos.y + 1);
                    }
                    else if (pieceCurrentField.Pos.x == followPieceField.Pos.x + 1)
                    {
                        targetPos = (pieceCurrentField.Pos.x - 1, pieceCurrentField.Pos.y + 1);
                    }
                    else if (pieceCurrentField.Pos.x == followPieceField.Pos.x - 1)
                    {
                        targetPos = (pieceCurrentField.Pos.x + 1, pieceCurrentField.Pos.y + 1);
                    }
                }

                if (targetPos != pieceCurrentField.Pos)
                {
                    var targetField = GetOrCreateField(targetPos);
                    pieceCurrentField.Pieces.Remove(piece);
                    targetField.Pieces.Add(piece);

                    if(piece == 9)
                        targetField.IsVisited= true;
                }
            }
        }

        private Field2 GetOrCreateField((int x, int y) pos)
        {
            var field = Fields.FirstOrDefault(x => x.Pos == pos);
            if (field == null)
            {
                Fields.Add(new Field2
                {
                    Pos = pos
                });
            }

            return Fields.Single(x => x.Pos == pos);
        }
    }

    class Game2
    {
        public Board2 Board { get; set; }

        public Game2()
        {
            Board = new Board2();
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
}
