namespace Aquifer.API.Endpoints.Bibles.Versification;

public class Request
{
    public int TargetBibleId { get; set; } // Always required
    public int BookId { get; set; }        // Always required
    public int? StartChapter { get; set; } // required for specific start chapter versification, or if EndChapter not null
    public int? EndChapter { get; set; }   // required for specific chapter or chapter span versification
    public int? StartVerse { get; set; }   // required for specific start chapter versification, or if EndVerse not null
    public int? EndVerse { get; set; }     // required for specific verse or verse span versification
}

// All linked passages in the CMS are based on ENG. Versification will be against the ENG as source bible. 
// The request data is saying "give me this ENG passage in TargetBible".
// Server is responsible for determining the proper min/max chapter and verse numbers per versification request

// CMS has bible-passage-utils to parse and construct verseIds

// allow for entire bible map?                    all other params would be optional
// allow for entire book map?                     book number or code needed for target book
// allow for specific chapter map?                startChapter and EndChapter for target chapter needed
// allow for specific verse map?                  start chapter, end chapter, start verse and end verse needed
// allow for map spanning books and or chapters?  start and end chapter can differ for spanning chapters.
//                                                   If the source passage spans into a different book than requested,
//                                                   the versification will reflect that in the returned verse ids
// Batch mappings per target bible?               If CMS can pass an array of b/c/v, response can be verse ids or b/c/v