"use client";

import { Button, Container, Heading, Link, Text } from "@noahwright/design";
import SiteShell from "@/components/SiteShell";
import { ACTIVITIES } from "@/lib/site";
import { COPY } from "@/content/copy";

export default function ActivityPlaceholder({ slug }: { slug: string }) {
  const activity = ACTIVITIES.find((a) => a.slug === slug)!;

  return (
    <SiteShell>
      <Container padding="lg">
        <Heading level={1}>
          {activity.emoji} {activity.title}
        </Heading>
        <Text>{activity.description}</Text>
        <Text tone="muted">{COPY.placeholder.stillBuilding.text}</Text>
        <Link href="/">
          <Button variant="outline">{COPY.placeholder.backButton.text}</Button>
        </Link>
      </Container>
    </SiteShell>
  );
}
