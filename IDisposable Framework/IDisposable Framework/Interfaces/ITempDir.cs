using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDisposable_Framework.Interfaces
{
    using System;

    /// <summary>
    /// Reprezentuje tymczasowy katalog
    /// </summary>
    /// <remarks>
    /// konstruktor bezargumentowy        -> tworzenie katalogu w domyslnej dla systemu/użytkownika lokalizacji
    ///                                      Path.GetTempPath
    /// konstruktor z argumentem `string` -> tworzenie katalogu we wskazanym miejscu
    /// </remarks>
    public interface ITempDir : ITempElement
    {
        // property zwracające pełną ścieżkę dostępu do katalogu
        string DirPath { get; }

        // opróżnia katalog ze wszystkich jego elementów
        void Empty();

        // zwraca true, jeśli katalog jest pusty
        bool IsEmpty { get; }
    }
}
