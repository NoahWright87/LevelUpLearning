"use client";

import { Button, Container, Heading, Link, Text } from "@noahwright/design";
import SiteShell from "@/components/SiteShell";
import { ACTIVITIES } from "@/lib/site";

export default function ActivityPlaceholder({ slug }: { slug: string }) {
  const activity = ACTIVITIES.find((a) => a.slug === slug)!;

  return (
    <SiteShell>
      <Container padding="lg">
        <Heading level={1}>
          {activity.emoji} {activity.title}
        </Heading>
        <Text>{activity.description}</Text>
        <Text tone="muted">🚧 This activity is still being built. Check back soon!</Text>
        <Link href="/">
          <Button variant="outline">Back to activities</Button>
        </Link>
      </Container>
    </SiteShell>
  );
}
