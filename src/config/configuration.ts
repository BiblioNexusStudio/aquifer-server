export default () => ({
    port: parseInt(process.env.PORT, 10) || 3000,
    useCompression: process.env.USE_COMPRESSION === 'true',
    database: {
        type: 'mssql' as const,
        host: process.env.DATABASE_HOST,
        database: process.env.DATABASE_NAME,
        ...(process.env.USE_LOCAL_DB === 'true'
            ? {
                  username: 'sa',
                  password: 'Password!',
                  options: { encrypt: false },
              }
            : { authentication: { type: 'azure-active-directory-default' } as any }),
    },
});
