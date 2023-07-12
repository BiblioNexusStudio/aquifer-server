import { ResourceType } from '../entities/resource.entity';

export class CreateResourceDto {
    passageReference: string;
    type: ResourceType;
}
