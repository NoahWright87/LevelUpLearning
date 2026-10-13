import { LOGO_PATHS, LOGO_VIEWBOX } from "@/lib/logo";

export default function Logo() {
  return (
    <svg className="site-logo" viewBox={LOGO_VIEWBOX} aria-hidden="true" focusable="false">
      {LOGO_PATHS.map((path, index) => (
        <path
          key={index}
          className={`site-logo__${path.part}`}
          d={path.d}
          transform={path.transform}
          fillRule="evenodd"
          fill={path.strokeWidth ? "none" : undefined}
          strokeWidth={path.strokeWidth}
        />
      ))}
    </svg>
  );
}
