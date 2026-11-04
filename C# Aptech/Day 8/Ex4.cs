namespace MusicPlaylistDemo;

public sealed class Song(int id, string title, string artist, int duration, string genre)
{
    public int Id { get; } = id > 0 ? id : throw new ArgumentOutOfRangeException(nameof(id));
    public string Title { get; } = !string.IsNullOrWhiteSpace(title) ? title.Trim() : throw new ArgumentException("Title cannot be empty.", nameof(title));
    public string Artist { get; } = !string.IsNullOrWhiteSpace(artist) ? artist.Trim() : throw new ArgumentException("Artist cannot be empty.", nameof(artist));
    public int Duration { get; } = duration > 0 ? duration : throw new ArgumentOutOfRangeException(nameof(duration));
    public string Genre { get; } = !string.IsNullOrWhiteSpace(genre) ? genre.Trim() : throw new ArgumentException("Genre cannot be empty.", nameof(genre));

    public override string ToString() => $"""
        ID: {Id}
        Title: {Title}
        Artist: {Artist}
        Duration: {Duration / 60:D2}:{Duration % 60:D2}
        Genre: {Genre}
        """;
}

public sealed class Playlist<T> where T : Song
{
    private readonly List<T> _songs = [];

    public void AddSong(T song)
    {
        ArgumentNullException.ThrowIfNull(song);

        if (_songs.Any(s => s.Id == song.Id))
        {
            throw new ArgumentException($"A song with ID {song.Id} already exists.");
        }

        _songs.Add(song);
    }

    public bool RemoveSong(int id) => _songs.RemoveAll(song => song.Id == id) > 0;

    public IEnumerable<T> GetSongsByGenre(string genre)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(genre);

        foreach (var song in _songs)
        {
            if (song.Genre.Equals(genre.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                yield return song;
            }
        }
    }

    public IEnumerable<T> GetSongsInDurationRange(int min, int max)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(min);

        if (max < min)
        {
            throw new ArgumentException("Maximum duration must be greater than or equal to minimum duration.");
        }

        foreach (var song in _songs)
        {
            if (song.Duration >= min && song.Duration <= max)
            {
                yield return song;
            }
        }
    }

    public IEnumerable<T> GetSongsReversed()
    {
        for (int i = _songs.Count - 1; i >= 0; i--)
        {
            yield return _songs[i];
        }
    }

    public IEnumerable<T> GetAllSongs()
    {
        foreach (var song in _songs)
        {
            yield return song;
        }
    }
}

public static class Program
{
    public static void Main()
    {
        Playlist<Song> playlist = new();

        playlist.AddSong(new Song(1, "Shape of You", "Ed Sheeran", 234, "Pop"));
        playlist.AddSong(new Song(2, "Blinding Lights", "The Weeknd", 200, "Pop"));
        playlist.AddSong(new Song(3, "Bohemian Rhapsody", "Queen", 354, "Rock"));
        playlist.AddSong(new Song(4, "Hotel California", "Eagles", 391, "Rock"));
        playlist.AddSong(new Song(5, "Perfect", "Ed Sheeran", 263, "Pop"));
        playlist.AddSong(new Song(6, "Take Five", "Dave Brubeck", 324, "Jazz"));
        playlist.AddSong(new Song(7, "Autumn Leaves", "Bill Evans", 347, "Jazz"));
        playlist.AddSong(new Song(8, "Lose Yourself", "Eminem", 326, "Hip-Hop"));

        Console.WriteLine("========== ALL SONGS ==========");
        foreach (var song in playlist.GetAllSongs())
        {
            Console.WriteLine(song);
            Console.WriteLine();
        }

        Console.WriteLine("========== POP SONGS ==========");
        foreach (var song in playlist.GetSongsByGenre("Pop"))
        {
            Console.WriteLine(song);
            Console.WriteLine();
        }

        Console.WriteLine("========== SONGS BETWEEN 200 AND 300 SECONDS ==========");
        foreach (var song in playlist.GetSongsInDurationRange(200, 300))
        {
            Console.WriteLine(song);
            Console.WriteLine();
        }

        Console.WriteLine("========== REVERSED PLAYLIST ==========");
        foreach (var song in playlist.GetSongsReversed())
        {
            Console.WriteLine(song);
            Console.WriteLine();
        }

        Console.WriteLine("========== REMOVE SONG ==========");
        const int songIdToRemove = 3;

        Console.WriteLine(playlist.RemoveSong(songIdToRemove) ? $"Song with ID {songIdToRemove} removed successfully." : $"Song with ID {songIdToRemove} not found.");

        Console.WriteLine("\n========== PLAYLIST AFTER REMOVAL ==========");
        foreach (var song in playlist.GetAllSongs())
        {
            Console.WriteLine($"{song.Id}. {song.Title} - {song.Artist}");
        }
    }
}