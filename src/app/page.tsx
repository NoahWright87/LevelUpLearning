"use client";

import { Hero, Card, CardGrid, Container, Heading, Text, Pill } from "@noahwright/design";
import SiteShell from "@/components/SiteShell";
import { ACTIVITIES, SITE } from "@/lib/site";

export default function HomePage() {
  return (
    <SiteShell>
      <Hero
        background="subtle"
        bottomBorder="gradient"
        title={<Heading level={1}>⭐ {SITE.name}</Heading>}
        tagline={<Heading level={2}>{SITE.tagline}</Heading>}
        description={<Text>{SITE.description}</Text>}
      />

      <Container padding="lg">
        <Heading level={2}>Pick an activity</Heading>
        <CardGrid minCardWidth="220px">
          {ACTIVITIES.map((activity) => (
            <Card
              key={activity.slug}
              href={`/${activity.slug}`}
              title={`${activity.emoji} ${activity.title}`}
              interactive
              footer={activity.ready ? undefined : <Pill>Coming soon</Pill>}
            >
              <Text tone="muted">{activity.description}</Text>
            </Card>
          ))}
        </CardGrid>
      </Container>
    </SiteShell>
  );
}
