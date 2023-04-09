
import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
  overwrite: true,
  schema: "https://localhost:7172/graphql",
  generates: {
    "src/models/generated/graphql.ts": {
      plugins: ["typescript"]
    }
  }
};

export default config;
