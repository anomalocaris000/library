namespace LibraryApp;

public class Reader
{
    public int Id { get; set; }
    public string FullName { get; set; }

    public Reader(int id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }

    public string GetInfo()
    {
        return $"ID: {Id}, Читатель: {FullName}";
    }
}