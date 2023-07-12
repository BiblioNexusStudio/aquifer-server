import {
    Entity,
    Column,
    UpdateDateColumn,
    CreateDateColumn,
    PrimaryGeneratedColumn,
} from 'typeorm';

@Entity({ name: 'Passages' })
export class Passage {
    @PrimaryGeneratedColumn({ name: 'Id' })
    id: number;

    @Column({ type: 'int', name: 'Type' })
    type: PassageType;

    @Column({ name: 'StartBnVerse' })
    startBnVerse: number;

    @Column({ name: 'EndBnVerse' })
    endBnVerse: number;

    @CreateDateColumn({ name: 'CreateDate' })
    createDate: Date;

    @UpdateDateColumn({ name: 'UpdateDate' })
    updateDate: Date;
}

export enum PassageType {
    CCBT_ER = 0,
}
