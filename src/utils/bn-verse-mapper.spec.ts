import {
    bnVerseToBookChapterVerse,
    BookChapterVerse,
    bookChapterVerseToBnVerse,
    BOOKS,
} from './bn-verse-mapper';

describe('bookChapterVerseToBnVerse', () => {
    it('should return bnVerse given a valid BookChapterVerse', () => {
        const bcv = new BookChapterVerse(BOOKS.MIC, 1, 1);
        expect(bookChapterVerseToBnVerse(bcv)).toEqual(133001001);
    });
});

describe('bnVerseToBookChapterVerse', () => {
    it('should return bnVerse given a valid BookChapterVerse', () => {
        const bcv = new BookChapterVerse(BOOKS.MIC, 1, 1);
        expect(bnVerseToBookChapterVerse(133001001)).toEqual(bcv);
    });
});

describe('BookChapterVerse', () => {
    it('should throw error with invalid book', () => {
        expect(() => new BookChapterVerse('KYLE' as any, 1, 1)).toThrowError(/Invalid/);
    });
});
