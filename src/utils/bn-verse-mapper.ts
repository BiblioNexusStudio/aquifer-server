export const BOOKS = {
    GEN: 'GEN', // Genesis
    EXO: 'EXO', // Exodus
    LEV: 'LEV', // Leviticus
    NUM: 'NUM', // Numbers
    DEU: 'DEU', // Deuteronomy
    JOS: 'JOS', // Joshua
    JDG: 'JDG', // Judges
    RUT: 'RUT', // Ruth
    '1SA': '1SA', // 1 Samuel
    '2SA': '2SA', // 2 Samuel
    '1KI': '1KI', // 1 Kings
    '2KI': '2KI', // 2 Kings
    '1CH': '1CH', // 1 Chronicles
    '2CH': '2CH', // 2 Chronicles
    EZR: 'EZR', // Ezra
    NEH: 'NEH', // Nehemiah
    EST: 'EST', // Esther (Hebrew)
    JOB: 'JOB', // Job
    PSA: 'PSA', // Psalms
    PRO: 'PRO', // Proverbs
    ECC: 'ECC', // Ecclesiastes
    SNG: 'SNG', // Song of Songs
    ISA: 'ISA', // Isaiah
    JER: 'JER', // Jeremiah
    LAM: 'LAM', // Lamentations
    EZK: 'EZK', // Ezekiel
    DAN: 'DAN', // Daniel (Hebrew)
    HOS: 'HOS', // Hosea
    JOL: 'JOL', // Joel
    AMO: 'AMO', // Amos
    OBA: 'OBA', // Obadiah
    JON: 'JON', // Jonah
    MIC: 'MIC', // Micah
    NAM: 'NAM', // Nahum
    HAB: 'HAB', // Habakkuk
    ZEP: 'ZEP', // Zephaniah
    HAG: 'HAG', // Haggai
    ZEC: 'ZEC', // Zechariah
    MAL: 'MAL', // Malachi
    MAT: 'MAT', // Matthew
    MRK: 'MRK', // Mark
    LUK: 'LUK', // Luke
    JHN: 'JHN', // John
    ACT: 'ACT', // Acts
    ROM: 'ROM', // Romans
    '1CO': '1CO', // 1 Corinthians
    '2CO': '2CO', // 2 Corinthians
    GAL: 'GAL', // Galatians
    EPH: 'EPH', // Ephesians
    PHP: 'PHP', // Philippians
    COL: 'COL', // Colossians
    '1TH': '1TH', // 1 Thessalonians
    '2TH': '2TH', // 2 Thessalonians
    '1TI': '1TI', // 1 Timothy
    '2TI': '2TI', // 2 Timothy
    TIT: 'TIT', // Titus
    PHM: 'PHM', // Philemon
    HEB: 'HEB', // Hebrews
    JAS: 'JAS', // James
    '1PE': '1PE', // 1 Peter
    '2PE': '2PE', // 2 Peter
    '1JN': '1JN', // 1 John
    '2JN': '2JN', // 2 John
    '3JN': '3JN', // 3 John
    JUD: 'JUD', // Jude
    REV: 'REV', // Revelation
    TOB: 'TOB', // Tobit
    JDT: 'JDT', // Judith
    ESG: 'ESG', // Esther Greek
    WIS: 'WIS', // Wisdom of Solomon
    SIR: 'SIR', // Sirach
    BAR: 'BAR', // Baruch
    LJE: 'LJE', // Letter of Jeremiah
    S3Y: 'S3Y', // Song of the 3 Young Men
    SUS: 'SUS', // Susanna
    BEL: 'BEL', // Bel and the Dragon
    '1MA': '1MA', // 1 Maccabees
    '2MA': '2MA', // 2 Maccabees
    '3MA': '3MA', // 3 Maccabees
    '4MA': '4MA', // 4 Maccabees
    '1ES': '1ES', // 1 Esdras (Greek)
    '2ES': '2ES', // 2 Esdras (Latin)
    MAN: 'MAN', // Prayer of Manasseh
    PS2: 'PS2', // Psalm 151
    ODA: 'ODA', // Odae/Odes
    PSS: 'PSS', // Psalms of Solomon
} as const;

export type Book = (typeof BOOKS)[keyof typeof BOOKS];

const integerToBookMapping: Record<number, Book> = {
    1: BOOKS.GEN, // Genesis
    2: BOOKS.EXO, // Exodus
    3: BOOKS.LEV, // Leviticus
    4: BOOKS.NUM, // Numbers
    5: BOOKS.DEU, // Deuteronomy
    6: BOOKS.JOS, // Joshua
    7: BOOKS.JDG, // Judges
    8: BOOKS.RUT, // Ruth
    9: BOOKS['1SA'], // 1 Samuel
    10: BOOKS['2SA'], // 2 Samuel
    11: BOOKS['1KI'], // 1 Kings
    12: BOOKS['2KI'], // 2 Kings
    13: BOOKS['1CH'], // 1 Chronicles
    14: BOOKS['2CH'], // 2 Chronicles
    15: BOOKS.EZR, // Ezra
    16: BOOKS.NEH, // Nehemiah
    17: BOOKS.EST, // Esther (Hebrew)
    18: BOOKS.JOB, // Job
    19: BOOKS.PSA, // Psalms
    20: BOOKS.PRO, // Proverbs
    21: BOOKS.ECC, // Ecclesiastes
    22: BOOKS.SNG, // Song of Songs
    23: BOOKS.ISA, // Isaiah
    24: BOOKS.JER, // Jeremiah
    25: BOOKS.LAM, // Lamentations
    26: BOOKS.EZK, // Ezekiel
    27: BOOKS.DAN, // Daniel (Hebrew)
    28: BOOKS.HOS, // Hosea
    29: BOOKS.JOL, // Joel
    30: BOOKS.AMO, // Amos
    31: BOOKS.OBA, // Obadiah
    32: BOOKS.JON, // Jonah
    33: BOOKS.MIC, // Micah
    34: BOOKS.NAM, // Nahum
    35: BOOKS.HAB, // Habakkuk
    36: BOOKS.ZEP, // Zephaniah
    37: BOOKS.HAG, // Haggai
    38: BOOKS.ZEC, // Zechariah
    39: BOOKS.MAL, // Malachi
    41: BOOKS.MAT, // Matthew
    42: BOOKS.MRK, // Mark
    43: BOOKS.LUK, // Luke
    44: BOOKS.JHN, // John
    45: BOOKS.ACT, // Acts
    46: BOOKS.ROM, // Romans
    47: BOOKS['1CO'], // 1 Corinthians
    48: BOOKS['2CO'], // 2 Corinthians
    49: BOOKS.GAL, // Galatians
    50: BOOKS.EPH, // Ephesians
    51: BOOKS.PHP, // Philippians
    52: BOOKS.COL, // Colossians
    53: BOOKS['1TH'], // 1 Thessalonians
    54: BOOKS['2TH'], // 2 Thessalonians
    55: BOOKS['1TI'], // 1 Timothy
    56: BOOKS['2TI'], // 2 Timothy
    57: BOOKS.TIT, // Titus
    58: BOOKS.PHM, // Philemon
    59: BOOKS.HEB, // Hebrews
    60: BOOKS.JAS, // James
    61: BOOKS['1PE'], // 1 Peter
    62: BOOKS['2PE'], // 2 Peter
    63: BOOKS['1JN'], // 1 John
    64: BOOKS['2JN'], // 2 John
    65: BOOKS['3JN'], // 3 John
    66: BOOKS.JUD, // Jude
    67: BOOKS.REV, // Revelation
    68: BOOKS.TOB, // Tobit
    69: BOOKS.JDT, // Judith
    70: BOOKS.ESG, // Esther Greek
    71: BOOKS.WIS, // Wisdom of Solomon
    72: BOOKS.SIR, // Sirach
    73: BOOKS.BAR, // Baruch
    74: BOOKS.LJE, // Letter of Jeremiah
    75: BOOKS.S3Y, // Song of the 3 Young Men
    76: BOOKS.SUS, // Susanna
    77: BOOKS.BEL, // Bel and the Dragon
    78: BOOKS['1MA'], // 1 Maccabees
    79: BOOKS['2MA'], // 2 Maccabees
    80: BOOKS['3MA'], // 3 Maccabees
    81: BOOKS['4MA'], // 4 Maccabees
    82: BOOKS['1ES'], // 1 Esdras (Greek)
    83: BOOKS['2ES'], // 2 Esdras (Latin)
    84: BOOKS.MAN, // Prayer of Manasseh
    85: BOOKS.PS2, // Psalm 151
    86: BOOKS.ODA, // Odae/Odes
    87: BOOKS.PSS, // Psalms of Solomon
} as const;

const bookToIntegerMapping: Record<Book, number> = {
    GEN: 1, // Genesis
    EXO: 2, // Exodus
    LEV: 3, // Leviticus
    NUM: 4, // Numbers
    DEU: 5, // Deuteronomy
    JOS: 6, // Joshua
    JDG: 7, // Judges
    RUT: 8, // Ruth
    '1SA': 9, // 1 Samuel
    '2SA': 10, // 2 Samuel
    '1KI': 11, // 1 Kings
    '2KI': 12, // 2 Kings
    '1CH': 13, // 1 Chronicles
    '2CH': 14, // 2 Chronicles
    EZR: 15, // Ezra
    NEH: 16, // Nehemiah
    EST: 17, // Esther (Hebrew)
    JOB: 18, // Job
    PSA: 19, // Psalms
    PRO: 20, // Proverbs
    ECC: 21, // Ecclesiastes
    SNG: 22, // Song of Songs
    ISA: 23, // Isaiah
    JER: 24, // Jeremiah
    LAM: 25, // Lamentations
    EZK: 26, // Ezekiel
    DAN: 27, // Daniel (Hebrew)
    HOS: 28, // Hosea
    JOL: 29, // Joel
    AMO: 30, // Amos
    OBA: 31, // Obadiah
    JON: 32, // Jonah
    MIC: 33, // Micah
    NAM: 34, // Nahum
    HAB: 35, // Habakkuk
    ZEP: 36, // Zephaniah
    HAG: 37, // Haggai
    ZEC: 38, // Zechariah
    MAL: 39, // Malachi
    MAT: 41, // Matthew
    MRK: 42, // Mark
    LUK: 43, // Luke
    JHN: 44, // John
    ACT: 45, // Acts
    ROM: 46, // Romans
    '1CO': 47, // 1 Corinthians
    '2CO': 48, // 2 Corinthians
    GAL: 49, // Galatians
    EPH: 50, // Ephesians
    PHP: 51, // Philippians
    COL: 52, // Colossians
    '1TH': 53, // 1 Thessalonians
    '2TH': 54, // 2 Thessalonians
    '1TI': 55, // 1 Timothy
    '2TI': 56, // 2 Timothy
    TIT: 57, // Titus
    PHM: 58, // Philemon
    HEB: 59, // Hebrews
    JAS: 60, // James
    '1PE': 61, // 1 Peter
    '2PE': 62, // 2 Peter
    '1JN': 63, // 1 John
    '2JN': 64, // 2 John
    '3JN': 65, // 3 John
    JUD: 66, // Jude
    REV: 67, // Revelation
    TOB: 68, // Tobit
    JDT: 69, // Judith
    ESG: 70, // Esther Greek
    WIS: 71, // Wisdom of Solomon
    SIR: 72, // Sirach
    BAR: 73, // Baruch
    LJE: 74, // Letter of Jeremiah
    S3Y: 75, // Song of the 3 Young Men
    SUS: 76, // Susanna
    BEL: 77, // Bel and the Dragon
    '1MA': 78, // 1 Maccabees
    '2MA': 79, // 2 Maccabees
    '3MA': 80, // 3 Maccabees
    '4MA': 81, // 4 Maccabees
    '1ES': 82, // 1 Esdras (Greek)
    '2ES': 83, // 2 Esdras (Latin)
    MAN: 84, // Prayer of Manasseh
    PS2: 85, // Psalm 151
    ODA: 86, // Odae/Odes
    PSS: 87, // Psalms of Solomon
} as const;

export class BookChapterVerse {
    book: Book;
    chapter: number;
    verse: number;

    constructor(book: Book, chapter: number, verse: number) {
        if (!(book in BOOKS)) {
            throw new Error('Invalid book');
        }
        this.book = book;
        this.chapter = chapter;
        this.verse = verse;
    }

    toString(): string {
        return `${this.book} ${this.chapter}:${this.verse}`;
    }
}

export function bookChapterVerseToBnVerse(bookChapterVerse: BookChapterVerse): number {
    const stringified =
        '1' +
        bookToIntegerMapping[bookChapterVerse.book].toString().padStart(2, '0') +
        bookChapterVerse.chapter.toString().padStart(3, '0') +
        bookChapterVerse.verse.toString().padStart(3, '0');
    return parseInt(stringified, 10);
}

export function bnVerseToBookChapterVerse(bnVerse: number): BookChapterVerse {
    const stringified = bnVerse.toString();
    const bookNumber = parseInt(stringified.substring(1, 3), 10);
    const chapterNumber = parseInt(stringified.substring(3, 6), 10);
    const verseNumber = parseInt(stringified.substring(6, 9), 10);
    return new BookChapterVerse(integerToBookMapping[bookNumber], chapterNumber, verseNumber);
}
