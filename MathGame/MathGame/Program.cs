// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Diagnostics;

Random roll = new Random();
List<GameResult> gameHistory = new List<GameResult>();

int gameNumber = 0;
int numberRange = 11;
int numberOfQuestions = 5;
int correctAnswer = 0;

string elapsedTime;

Stopwatch sw;

DifficultyLevel(out numberRange, out numberOfQuestions);

int a = GenerateNumber();
int b = GenerateNumber();

TrackTime(out sw);
PickOperator(numberOfQuestions, a, b);
ShowScore(sw, numberOfQuestions, correctAnswer);

int GenerateNumber()
{
    return roll.Next(1, numberRange);
}

void CheckAnswer(int result)
{
    string? input = Console.ReadLine();
    if (int.TryParse(input, out int userAnswer) && userAnswer == result)
    {
        Console.WriteLine("You got it right!");
        correctAnswer++;
    }

    else
        Console.WriteLine("Wrong!");
}

void PickOperator(int numberOfQuestions, int a, int b)
{
    string? input;

    while (true)
    {
        Console.WriteLine("Pick an operator: 1) Add 2) Subtract 3) Multiply 4) Division 5) Random");
        input = Console.ReadLine();

        if (input == "1" || input == "2" || input == "3" || input == "4" || input == "5")
            break;

        Console.WriteLine("Error: Not valid!");
    }

    //Make menu and Ask player to pick an operator
    switch (input)
    {

        case "1":
            for (int i = 0; i < numberOfQuestions; i++)
            {
                int result = a + b;
                Console.WriteLine($"What is the result of: {a} + {b}?");
                CheckAnswer(result);

                a = GenerateNumber();
                b = GenerateNumber();
            }
            break;

        case "2":
            for (int i = 0; i < numberOfQuestions; i++)
            {
                int result = Math.Abs(a - b);
                Console.WriteLine($"What is the result of: {a} - {b}?");
                CheckAnswer(result);

                a = GenerateNumber();
                b = GenerateNumber();
            }
            break;

        case "3":
            for (int i = 0; i < numberOfQuestions; i++)
            {
                int result = a * b;
                Console.WriteLine($"What is the result of: {a} * {b}?");
                CheckAnswer(result);

                a = GenerateNumber();
                b = GenerateNumber();
            }
            break;

        case "4":
            for (int i = 0; i < numberOfQuestions; i++)
            {
                while (a % b != 0)
                {
                    a = GenerateNumber();
                    b = GenerateNumber();
                }

                int result = a / b;

                Console.WriteLine($"What is the result of: {a} / {b}?");
                CheckAnswer(result);

                a = GenerateNumber();
                b = GenerateNumber();
            }
            break;

        case "5":

            for (int i = 0; i < numberOfQuestions; i++)
            {
                string randomOp = roll.Next(1, 5).ToString();
                switch (randomOp)
                {
                    case "1":
                        {
                            int result = a + b;
                            Console.WriteLine($"What is the result of: {a} + {b}?");
                            CheckAnswer(result);

                            a = GenerateNumber();
                            b = GenerateNumber();
                            break;
                        }

                    case "2":
                        {
                            int result = Math.Abs(a - b);
                            Console.WriteLine($"What is the result of: {a} - {b}?");
                            CheckAnswer(result);

                            a = GenerateNumber();
                            b = GenerateNumber();
                            break;
                        }

                    case "3":
                        {
                            int result = a * b;
                            Console.WriteLine($"What is the result of: {a} * {b}?");
                            CheckAnswer(result);

                            a = GenerateNumber();
                            b = GenerateNumber();
                            break;
                        }

                    case "4":
                        {
                            while (a % b != 0)
                            {
                                a = GenerateNumber();
                                b = GenerateNumber();
                            }

                            int result = a / b;

                            Console.WriteLine($"What is the result of: {a} / {b}?");
                            CheckAnswer(result);

                            a = GenerateNumber();
                            b = GenerateNumber();
                            break;
                        }
                }
            }

            break;
    }
}

void ShowScore(in Stopwatch sw, int numberOfQuestions, int correctAnswer)
{
    sw.Stop();
    TimeSpan ts = sw.Elapsed;

    gameNumber++;
    elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
    ts.Hours, ts.Minutes, ts.Seconds);
    
    Console.WriteLine($"Congratulations! You finished with {correctAnswer} of {numberOfQuestions} answers correct!");
    Console.WriteLine($"It took you a total time of: {elapsedTime}");
    Console.WriteLine("Show game history? Y/N");
    Console.WriteLine("");

    while (true)
    {
        string? gameHistoryInput = Console.ReadLine();

        if (!string.IsNullOrEmpty(gameHistoryInput))
        {
            char c = gameHistoryInput[0];

            if (c.ToString().ToUpper() == "Y")
            {
                SaveGameResult();
                GameHistory();
                PlayAgain();
                return;
            }

            else if (c.ToString().ToUpper() == "N")
            {
                SaveGameResult();
                PlayAgain();
                return;
            }

            Console.WriteLine("Error: Not valid!");
        }
    }
}

void DifficultyLevel(out int numberRange, out int numberOfQuestions)
{
    string? input;
    numberRange = 11;
    numberOfQuestions = 5;

    while (true)
    {
        Console.WriteLine("Pick a difficulty: 1) Easy\t2) Medium\t3) Hard\t\t4) Very Hard");
        input = Console.ReadLine();
        if (input == "1" || input == "2" || input == "3" || input == "4")
            break;

        Console.WriteLine("Error: Not valid!");
    }

    switch (input)
    {
        case "1":
            numberRange = 11;
            numberOfQuestions = 5;
            break;

        case "2":
            numberRange = 26;
            numberOfQuestions = 5;
            break;

        case "3":
            numberRange = 101;
            numberOfQuestions = 10;
            break;

        case "4":
            numberRange = 251;
            numberOfQuestions = 15;
            break;
    }
}

void TrackTime(out Stopwatch sw)
{
    sw = new Stopwatch();
    sw.Start();
}

void PlayAgain()
{
    while (true)
    {
        Console.WriteLine("Play again? Y/N");
        string? replayInput = Console.ReadLine();

        if (!string.IsNullOrEmpty(replayInput))
        {
            char c = replayInput[0];

            if (c.ToString().ToUpper() == "Y")
            {
                //restarts game
                correctAnswer = 0;

                DifficultyLevel(out numberRange, out numberOfQuestions);

                int a = GenerateNumber();
                int b = GenerateNumber();

                TrackTime(out sw);
                PickOperator(numberOfQuestions, a, b);
                ShowScore(sw, numberOfQuestions, correctAnswer);
                return;
            }

            else if (c.ToString().ToUpper() == "N")
            {
                Environment.Exit(0);
            }
        }

        Console.WriteLine("Error: Not valid!");
    }
}

void SaveGameResult()
{
    gameHistory.Add(new GameResult
    {
        GameNumber = gameNumber,
        CorrectAnswers = correctAnswer,
        TotalQuestions = numberOfQuestions,
        ElapsedTime = elapsedTime

    });
}

void GameHistory()
{
    foreach (var game in gameHistory)
    {
        Console.WriteLine($"Game {game.GameNumber} Results:");
        Console.WriteLine($"Correct answers: {game.CorrectAnswers} of {game.TotalQuestions}");
        Console.WriteLine($"Time: {game.ElapsedTime}");
        Console.WriteLine("");
    }
}

public class GameResult
{
    public int GameNumber { get; set; }
    public int CorrectAnswers { get; set; }
    public int TotalQuestions { get; set; }
    public string? ElapsedTime { get; set; }
}

