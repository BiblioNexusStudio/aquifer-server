import { PassageType } from '../entities/passage.entity';

export class CreatePassageDto {
    passageReference: string;
    type: PassageType;
}
