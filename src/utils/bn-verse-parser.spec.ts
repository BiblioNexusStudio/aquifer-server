import { BookChapterVerse, BOOKS } from './bn-verse-mapper';
import { parsePassageReference } from './bn-verse-parser';

describe('parsePassageReference', () => {
    it('should return start and end BookChapterVerse', () => {
        const [start, end] = parsePassageReference('Habakkuk 1:2-2:3');
        expect(start).toEqual(new BookChapterVerse(BOOKS.HAB, 1, 2));
        expect(end).toEqual(new BookChapterVerse(BOOKS.HAB, 2, 3));
    });

    it('should return null if unparseable', () => {
        expect(parsePassageReference('uh oh')).toBeNull();
    });
});
