import { ResourceType } from '../entities/resource.entity';

export class CreateResourceContentDto {
    passageReference: string;
    type: ResourceType;
    content: object;
    displayName: string; // display name for the resource in the same language as languageCode
    languageCode: string; // three character ISO639_3Code
}
