import { Controller, Get, Param, ParseArrayPipe, Query } from '@nestjs/common';
import { ResourceType, ResourceTypeInt } from './entities/resource.entity';
import { PassagesService } from './passages.service';

@Controller('passages')
export class PassagesController {
    constructor(private readonly passagesService: PassagesService) {}

    @Get('languages/:languageCode')
    async findAllWithResourcesInLanguage(
        @Param('languageCode') languageCode: string,
        @Query('resourceTypes', ParseArrayPipe) resourceTypes: ResourceType[],
    ) {
        const passages = (
            await this.passagesService.findAllWithResourcesInLanguage(resourceTypes, languageCode)
        )
            .map((p) => ({
                start: p.startBookChapterVerse,
                end: p.endBookChapterVerse,
                resources: p.resources.flatMap((resource) =>
                    resource.resourceContents.map(({ content }) => ({
                        content,
                        type: ResourceTypeInt[resource.type],
                    })),
                ),
            }))
            .groupBy((item) => item.start.book);
        return passages;
    }

    @Get(':id')
    findOne(@Param('id') id: string) {
        return this.passagesService.findOne(+id);
    }
}
