namespace ConsoleApp2
{
    public static class Explorer
    {
        public static string currentPath;
        public static DriveInfo[] drives;


        public static void Run()
        {
            LoadDrives();
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Выберите диск:");

                for (int i = 0; i < drives.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {drives[i].Name}");
                }

                Console.WriteLine("Escape. Выход");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (int.TryParse(keyInfo.KeyChar.ToString(), out int choice) && choice >= 1 && choice <= drives.Length)
                {
                    currentPath = drives[choice - 1].RootDirectory.FullName;
                    ShowFolder(currentPath);
                }
                else
                {
                    Console.WriteLine("Неверный выбор");
                    Console.ReadKey();
                }
            }
        }

        private static void LoadDrives()
        {
            drives = DriveInfo.GetDrives();
        }

        private static void ShowFolder(string path)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"Текущая папка: {path}");
                Console.WriteLine();

                try
                {
                    string[] directories = Directory.GetDirectories(path);
                    string[] files = Directory.GetFiles(path);

                    Console.WriteLine("Папки:");
                    for (int i = 0; i < directories.Length; i++)
                    {
                        string folderName = Path.GetFileName(directories[i]);
                        Console.WriteLine($"{i + 1}. {folderName}");
                    }

                    Console.WriteLine();

                    Console.WriteLine("Файлы:");
                    for (int i = 0; i < files.Length; i++)
                    {
                        string fileName = Path.GetFileName(files[i]);
                        Console.WriteLine($"{i + 1 + directories.Length}. {fileName}");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Выберите папку или файл для просмотра:");
                    Console.WriteLine("Escape. Назад");

                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        string parentFolder = Directory.GetParent(path)?.FullName;
                        if (parentFolder != null && !string.IsNullOrEmpty(parentFolder))
                        {
                            ShowFolder(parentFolder);
                            return;
                        }
                    }
                    else if (int.TryParse(keyInfo.KeyChar.ToString(), out int choice) && choice >= 1 && choice <= directories.Length + files.Length)
                    {
                        string selectedPath = choice <= directories.Length ? directories[choice - 1] : files[choice - 1 - directories.Length];

                        if (Directory.Exists(selectedPath))
                        {
                            ShowFolder(selectedPath);
                        }
                        else if (File.Exists(selectedPath))
                        {
                            OpenFile(selectedPath);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }

        private static void OpenFile(string path)
        {
            try
            {
                Console.WriteLine($"Открытие файла: {path}");
                // Здесь можно реализовать открытие файла с помощью соответствующей программы на компьютере
                // Например: Process.Start(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось открыть файл: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Explorer.Run();
        }
    }
}