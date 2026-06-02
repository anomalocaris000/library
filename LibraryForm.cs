namespace LibraryApp;

using System.Windows.Forms;

public class LibraryForm : Form
{
    private FileStorage storage;
    private Library library;

    private Button btnLoadText;
    private Button btnSaveText;
    private Button btnLoadJson;
    private Button btnSaveJson;
    private TextBox txtOutput;
    private Label lblStatus;

    public LibraryForm()
    {
        storage = new FileStorage();
        library = new Library();

        InitializeComponent();
        LoadInitialData();
    }

    private void InitializeComponent()
    {
        this.Text = "Библиотека - Управление данными";
        this.Width = 800;
        this.Height = 600;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Font = new Font("Arial", 10);

        // Создание панели с кнопками
        Panel pnlButtons = new Panel();
        pnlButtons.Dock = DockStyle.Top;
        pnlButtons.Height = 80;
        pnlButtons.Padding = new Padding(10);
        pnlButtons.BackColor = Color.LightGray;

        // Кнопка загрузки текстовых файлов
        btnLoadText = new Button();
        btnLoadText.Text = "Загрузить TXT";
        btnLoadText.Width = 150;
        btnLoadText.Height = 35;
        btnLoadText.Location = new Point(10, 10);
        btnLoadText.BackColor = Color.LightBlue;
        btnLoadText.Click += BtnLoadText_Click;
        pnlButtons.Controls.Add(btnLoadText);

        // Кнопка сохранения текстовых файлов
        btnSaveText = new Button();
        btnSaveText.Text = "Сохранить TXT";
        btnSaveText.Width = 150;
        btnSaveText.Height = 35;
        btnSaveText.Location = new Point(170, 10);
        btnSaveText.BackColor = Color.LightGreen;
        btnSaveText.Click += BtnSaveText_Click;
        pnlButtons.Controls.Add(btnSaveText);

        // Кнопка загрузки JSON файлов
        btnLoadJson = new Button();
        btnLoadJson.Text = "Загрузить JSON";
        btnLoadJson.Width = 150;
        btnLoadJson.Height = 35;
        btnLoadJson.Location = new Point(330, 10);
        btnLoadJson.BackColor = Color.LightBlue;
        btnLoadJson.Click += BtnLoadJson_Click;
        pnlButtons.Controls.Add(btnLoadJson);

        // Кнопка сохранения JSON файлов
        btnSaveJson = new Button();
        btnSaveJson.Text = "Сохранить JSON";
        btnSaveJson.Width = 150;
        btnSaveJson.Height = 35;
        btnSaveJson.Location = new Point(490, 10);
        btnSaveJson.BackColor = Color.LightGreen;
        btnSaveJson.Click += BtnSaveJson_Click;
        pnlButtons.Controls.Add(btnSaveJson);

        // Создание статус-лабели
        lblStatus = new Label();
        lblStatus.Text = "Статус: готово";
        lblStatus.Location = new Point(10, 50);
        lblStatus.Width = 700;
        lblStatus.Height = 20;
        lblStatus.ForeColor = Color.DarkGreen;
        pnlButtons.Controls.Add(lblStatus);

        this.Controls.Add(pnlButtons);

        // Создание текстбокса для вывода
        txtOutput = new TextBox();
        txtOutput.Dock = DockStyle.Fill;
        txtOutput.Multiline = true;
        txtOutput.ReadOnly = true;
        txtOutput.ScrollBars = ScrollBars.Both;
        txtOutput.BackColor = Color.WhiteSmoke;
        txtOutput.Font = new Font("Courier New", 9);
        this.Controls.Add(txtOutput);
    }

    private void LoadInitialData()
    {
        try
        {
            var books = storage.LoadBooksFromText();
            var readers = storage.LoadReadersFromText();
            var issues = storage.LoadIssuesFromText();
            var returns = storage.LoadReturnsFromText();

            foreach (var book in books)
                library.AddBook(book);

            foreach (var reader in readers)
                library.AddReader(reader);

            foreach (var issue in issues)
                library.AddIssue(issue.BookId, issue.ReaderId, issue.Date);

            foreach (var ret in returns)
                library.AddReturn(ret.BookId, ret.ReaderId, ret.Date);

            if (library.GetBooks().Count > 0)
            {
                DisplayMessage("Начальные данные загружены из текстовых файлов", true);
                DisplayLibraryInfo();
            }
        }
        catch (Exception ex)
        {
            DisplayMessage($"Ошибка при загрузке начальных данных: {ex.Message}", false);
        }
    }

    private void BtnLoadText_Click(object sender, EventArgs e)
    {
        try
        {
            library.ClearData();

            var books = storage.LoadBooksFromText();
            var readers = storage.LoadReadersFromText();
            var issues = storage.LoadIssuesFromText();
            var returns = storage.LoadReturnsFromText();

            foreach (var book in books)
                library.AddBook(book);

            foreach (var reader in readers)
                library.AddReader(reader);

            foreach (var issue in issues)
                library.AddIssue(issue.BookId, issue.ReaderId, issue.Date);

            foreach (var ret in returns)
                library.AddReturn(ret.BookId, ret.ReaderId, ret.Date);

            DisplayMessage("Данные успешно загружены из текстовых файлов", true);
            DisplayMessage($"Загружено книг: {books.Count}", true);
            DisplayMessage($"Загружено читателей: {readers.Count}", true);
            DisplayMessage($"Загружено выдач: {issues.Count}", true);
            DisplayMessage($"Загружено возвратов: {returns.Count}", true);
            DisplayLibraryInfo();
        }
        catch (Exception ex)
        {
            DisplayMessage($"Ошибка при загрузке TXT: {ex.Message}", false);
        }
    }

    private void BtnSaveText_Click(object sender, EventArgs e)
    {
        try
        {
            storage.SaveBooksToText(library.GetBooks());
            storage.SaveReadersToText(library.GetReaders());
            storage.SaveIssuesToText(library.GetIssues());
            storage.SaveReturnsToText(library.GetReturns());

            DisplayMessage("Данные успешно сохранены в текстовые файлы", true);
            DisplayMessage("Папка: data/", true);
            DisplayMessage("Файлы: books.txt, readers.txt, issues.txt, returns.txt", true);
        }
        catch (Exception ex)
        {
            DisplayMessage($"Ошибка при сохранении TXT: {ex.Message}", false);
        }
    }

    private void BtnLoadJson_Click(object sender, EventArgs e)
    {
        try
        {
            library.ClearData();

            var books = storage.LoadBooksFromJson();
            var readers = storage.LoadReadersFromJson();
            var issues = storage.LoadIssuesFromJson();
            var returns = storage.LoadReturnsFromJson();

            foreach (var book in books)
                library.AddBook(book);

            foreach (var reader in readers)
                library.AddReader(reader);

            foreach (var issue in issues)
                library.AddIssue(issue.BookId, issue.ReaderId, issue.Date);

            foreach (var ret in returns)
                library.AddReturn(ret.BookId, ret.ReaderId, ret.Date);

            DisplayMessage("Данные успешно загружены из JSON файлов", true);
            DisplayMessage($"Загружено книг: {books.Count}", true);
            DisplayMessage($"Загружено читателей: {readers.Count}", true);
            DisplayMessage($"Загружено выдач: {issues.Count}", true);
            DisplayMessage($"Загружено возвратов: {returns.Count}", true);
            DisplayLibraryInfo();
        }
        catch (Exception ex)
        {
            DisplayMessage($"Ошибка при загрузке JSON: {ex.Message}", false);
        }
    }

    private void BtnSaveJson_Click(object sender, EventArgs e)
    {
        try
        {
            storage.SaveBooksToJson(library.GetBooks());
            storage.SaveReadersToJson(library.GetReaders());
            storage.SaveIssuesToJson(library.GetIssues());
            storage.SaveReturnsToJson(library.GetReturns());

            DisplayMessage("Данные успешно сохранены в JSON файлы", true);
            DisplayMessage("Папка: data/", true);
            DisplayMessage("Файлы: books.json, readers.json, issues.json, returns.json", true);
        }
        catch (Exception ex)
        {
            DisplayMessage($"Ошибка при сохранении JSON: {ex.Message}", false);
        }
    }

    private void DisplayLibraryInfo()
    {
        DisplayMessage("", true);
        DisplayMessage(new string('=', 60), true);
        DisplayMessage("ТЕКУЩЕЕ СОСТОЯНИЕ БИБЛИОТЕКИ", true);
        DisplayMessage(new string('=', 60), true);

        DisplayMessage("", true);
        DisplayMessage("КНИГИ:", true);
        if (library.GetBooks().Count == 0)
        {
            DisplayMessage("Нет книг", true);
        }
        else
        {
            foreach (var book in library.GetBooks())
            {
                string status = book.IsAvailable ? "Доступна" : "Выдана";
                DisplayMessage($"{book.GetInfo()} | {status}", true);
            }
        }

        DisplayMessage("", true);
        DisplayMessage("ЧИТАТЕЛИ:", true);
        if (library.GetReaders().Count == 0)
        {
            DisplayMessage("Нет читателей", true);
        }
        else
        {
            foreach (var reader in library.GetReaders())
            {
                DisplayMessage($"{reader.GetInfo()}", true);
            }
        }

        DisplayMessage("", true);
        DisplayMessage("ИСТОРИЯ ВЫДАЧИ:", true);
        if (library.GetIssues().Count == 0)
        {
            DisplayMessage("Нет выдач", true);
        }
        else
        {
            foreach (var issue in library.GetIssues())
            {
                var book = library.FindBook(issue.BookId);
                var reader = library.FindReader(issue.ReaderId);
                if (book != null && reader != null)
                {
                    DisplayMessage($"Книга '{book.Title}' выдана {reader.FullName} {issue.Date:dd.MM.yyyy HH:mm}", true);
                }
            }
        }

        DisplayMessage("", true);
        DisplayMessage("ИСТОРИЯ ВОЗВРАТА:", true);
        if (library.GetReturns().Count == 0)
        {
            DisplayMessage("Нет возвратов", true);
        }
        else
        {
            foreach (var ret in library.GetReturns())
            {
                var book = library.FindBook(ret.BookId);
                var reader = library.FindReader(ret.ReaderId);
                if (book != null && reader != null)
                {
                    DisplayMessage($"Книга '{book.Title}' возвращена {reader.FullName} {ret.Date:dd.MM.yyyy HH:mm}", true);
                }
            }
        }

        DisplayMessage("", true);
        DisplayMessage(new string('=', 60), true);
    }

    private void DisplayMessage(string message, bool success)
    {
        if (string.IsNullOrEmpty(message))
        {
            txtOutput.AppendText(Environment.NewLine);
        }
        else
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string prefix = success ? "[OK]" : "[ERROR]";
            txtOutput.AppendText($"{prefix} {timestamp} : {message}{Environment.NewLine}");
        }

        lblStatus.Text = $"Статус: {message}";
        lblStatus.ForeColor = success ? Color.DarkGreen : Color.DarkRed;
    }
}
