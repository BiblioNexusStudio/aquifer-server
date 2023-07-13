import * as request from 'supertest';
import { ResourceContentService } from './resources/resource-content.service';
import { app } from './test-utils/setup-integration-tests';

describe('Passages (integration)', () => {
    describe('/passages/languages/:languageCode (GET)', () => {
        it('returns nothing when none match', async () => {
            const response = await request(app.getHttpServer())
                .get('/passages/languages/eng?resourceTypes[]=CBBT_ER')
                .expect(200);
            expect(response.body).toEqual({});
        });

        it('returns one when it finds a match', async () => {
            await app.get(ResourceContentService).upsert({
                passageReference: 'Gen 1:1-5',
                type: 'CBBT_ER',
                content: { stuff: true },
                displayName: 'CBBT-ER Gen 1:1-5',
                languageCode: 'eng',
            });

            const response = await request(app.getHttpServer())
                .get('/passages/languages/eng?resourceTypes[]=CBBT_ER')
                .expect(200);

            expect(response.body).toMatchObject({
                GEN: [
                    {
                        end: {
                            book: 'GEN',
                            chapter: 1,
                            verse: 5,
                        },
                        resources: [
                            {
                                content: '{"stuff":true}',
                                type: 'CBBT_ER',
                            },
                        ],
                        start: {
                            book: 'GEN',
                            chapter: 1,
                            verse: 1,
                        },
                    },
                ],
            });
        });
    });
});
