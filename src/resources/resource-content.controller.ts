import { Body, Controller, Post } from '@nestjs/common';
import { CreateResourceContentDto } from './dto/create-resource-content.dto';
import { ResourceContentService } from './resource-content.service';

@Controller('resource-content')
export class ResourceContentController {
    constructor(private readonly resourceContentService: ResourceContentService) {}

    // TODO: add authentication to this endpoint
    @Post()
    create(@Body() dto: CreateResourceContentDto) {
        return this.resourceContentService.upsert(dto);
    }
}
