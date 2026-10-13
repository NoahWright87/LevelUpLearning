import { FlatCompat } from "@eslint/eslintrc";
import { fileURLToPath } from "url";
import path from "path";

const __dirname = path.dirname(fileURLToPath(import.meta.url));
const compat = new FlatCompat({ baseDirectory: __dirname });

const eslintConfig = [
  ...compat.extends("next/core-web-vitals", "next/typescript"),
  {
    rules: {
      // An effect's return value is its cleanup, so an expression body like
      // `useEffect(() => window.scrollTo(...))` hands React whatever that call returns. Newer
      // Chrome returns a Promise from scrollTo, which crashed the spelling quiz. Use braces.
      "no-restricted-syntax": [
        "error",
        {
          selector:
            "CallExpression[callee.name=/^use(Layout|Insertion)?Effect$/] > ArrowFunctionExpression[body.type!='BlockStatement'][body.type!='ArrowFunctionExpression']",
          message: "Give effects a block body ({ ... }) so they can't return a non-function as their cleanup.",
        },
      ],
    },
  },
  {
    ignores: [".next/**", "out/**", "build/**", "next-env.d.ts"],
  },
];

export default eslintConfig;
