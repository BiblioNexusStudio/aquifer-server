import { Controller, Get, Param } from '@nestjs/common';
import { PassagesService } from './passages.service';

@Controller('passages')
export class PassagesController {
    constructor(private readonly passagesService: PassagesService) {}

    @Get()
    findAll() {
        return this.passagesService.findAll();
    }

    @Get(':id')
    findOne(@Param('id') id: string) {
        return this.passagesService.findOne(+id);
    }
}
