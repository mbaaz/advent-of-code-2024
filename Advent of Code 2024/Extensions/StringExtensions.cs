namespace AoC.Y24.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> SplitToLines(this string input, int maxLineLength)
    {
        if(string.IsNullOrWhiteSpace(input))
        {
            yield return string.Empty;
            yield break;
        }

        var naturalLines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
        foreach(var naturalLine in naturalLines)
        {
            // Split line into words
            var words = naturalLine.Split(' ');

            // Start line with first word
            var line = words.First();
            
            // If word is longer than allowed, then break it
            while (line.Length > maxLineLength)
            {
                yield return $"{line[..(maxLineLength-1)]}-";
                line = line[(maxLineLength - 1)..];
            }

            // Try to add words to line
            foreach (var word in words.Skip(1))
            {
                var test = $"{line} {word}";
                // Check if word can fit on current line
                if (test.Length > maxLineLength)
                {
                    // ... it cannot - return line and start new one
                    yield return line;
                    line = word;

                    // If word (line) is longer than allowed, then break it
                    while (line.Length > maxLineLength)
                    {
                        yield return $"{line[..(maxLineLength - 1)]}-";
                        line = line[(maxLineLength - 1)..];
                    }
                }
                else
                {
                    line = test;
                }
            }

            // Return last line
            yield return line;
        }
    }
}