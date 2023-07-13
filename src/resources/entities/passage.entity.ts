import { bnVerseToBookChapterVerse, BookChapterVerse } from '../../utils/bn-verse-mapper';
import {
    Entity,
    Column,
    UpdateDateColumn,
    CreateDateColumn,
    PrimaryGeneratedColumn,
    Index,
    ManyToMany,
    JoinTable,
} from 'typeorm';
import { Resource } from './resource.entity';
import { BNBaseEntity } from '../../utils/bn-base-entity';

@Entity({ name: 'Passages' })
@Index(['type', 'startBnVerse', 'endBnVerse'], { unique: true })
export class Passage extends BNBaseEntity {
    private _startBookChapterVerse: BookChapterVerse;
    private _endBookChapterVerse: BookChapterVerse;

    @PrimaryGeneratedColumn({ name: 'Id' })
    id: number;

    @Column({ type: 'int', name: 'Type' })
    type: PassageTypeInt;

    @ManyToMany(() => Resource, { onDelete: 'NO ACTION', onUpdate: 'NO ACTION' })
    @JoinTable({
        name: 'PassageResources',
        joinColumn: { name: 'PassageId' },
        inverseJoinColumn: { name: 'ResourceId' },
    })
    resources: Resource[];

    @Column({ name: 'StartBnVerse' })
    startBnVerse: number;

    @Column({ name: 'EndBnVerse' })
    endBnVerse: number;

    @CreateDateColumn({ name: 'CreateDate' })
    createDate: Date;

    @UpdateDateColumn({ name: 'UpdateDate' })
    updateDate: Date;

    get startBookChapterVerse() {
        if (!this._startBookChapterVerse) {
            this._startBookChapterVerse = bnVerseToBookChapterVerse(this.startBnVerse);
        }
        return this._startBookChapterVerse;
    }

    get endBookChapterVerse() {
        if (!this._endBookChapterVerse) {
            this._endBookChapterVerse = bnVerseToBookChapterVerse(this.endBnVerse);
        }
        return this._endBookChapterVerse;
    }

    toString(): string {
        return `${this.startBookChapterVerse}-${this.endBookChapterVerse}`;
    }
}

export enum PassageTypeInt {
    CBBT_ER = 0,
}

export type PassageType = keyof typeof PassageTypeInt;
