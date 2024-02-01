namespace ConcurrencyTricks
{
    /// <summary>
    /// "Мягкий таймаут"
    /// </summary>
    internal static class SoftTimeout
    {
        /// <summary>
        /// Версия в случае если методом не предусмотрен таймаут через CancellationToken
        /// </summary>
        /// <param name="uri">Путь к файлу</param>
        public static async Task<string?> ReadFileWithTimeout1(string uri)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            Task<string> readTask = File.ReadAllTextAsync(uri);
            Task timeoutTask = Task.Delay(Timeout.Infinite, cts.Token);

            Task completedTask = await Task.WhenAny(readTask, timeoutTask);
            if (completedTask == timeoutTask)
                return null;
            return await readTask;
        }

        /// <summary>
        /// Версия в случае, если методом предусмотрен таймаут через CancellationToken
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<string> ReadFileWithTimout2(string uri)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            string fileText = await File.ReadAllTextAsync(uri, cts.Token);

            return fileText;
        }
    }
}
