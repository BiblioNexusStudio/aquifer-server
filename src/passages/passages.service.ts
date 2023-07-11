import { Injectable } from '@nestjs/common';

@Injectable()
export class PassagesService {
    findAll() {
        return `This action returns all passages`;
    }

    findOne(id: number) {
        return `This action returns a #${id} passage`;
    }
}
