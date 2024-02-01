namespace ConcurrencyTricks
{
    /// <summary>
    /// Возвращение значения синхронно, в случае асинхронных методов
    /// </summary>
    internal static class ReturnCompletedTask
    {
        /// <summary>
        /// Вернуть значение
        /// </summary>
        public static Task<string> GetSomeValueAsync()
        {
            return Task.FromResult("someValue");
        }

        /// <summary>
        /// Вернуть кешированную завершённую задачу
        /// </summary>
        public static Task Do()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Вернуть исключение завершённой задачи
        /// </summary>
        public static Task<T> DoException<T>()
        {
            return Task.FromException<T>(new NotImplementedException());
        }

        /// <summary>
        /// Вернуть отменённую задачу, если IsCancellationRequest, иначе вернуть завершённую задачу со значением
        /// </summary>
        public static Task<string> GetVal(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Task.FromCanceled<string>(cancellationToken);
            return Task.FromResult("someValue");
        }
    }
}
