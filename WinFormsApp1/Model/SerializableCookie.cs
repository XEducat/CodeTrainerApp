namespace CodeTrainerApp.Model
{
    /// <summary>
    /// Клас для серіалізації cookie для збереження між сесіями.
    /// </summary>
    public class SerializableCookie
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string? Path { get; set; }
        public DateTime? Expires { get; set; }
        public bool Secure { get; set; }
        public bool HttpOnly { get; set; }
    }
}
