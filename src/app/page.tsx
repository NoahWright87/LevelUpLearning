"use client";

import { Hero, Card, CardGrid, Container, Heading, Text, Pill } from "@noahwright/design";
import SiteShell from "@/components/SiteShell";
import { ACTIVITIES, SITE } from "@/lib/site";
import { COPY } from "@/content/copy";

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
        <Heading level={2}>{COPY.home.activitiesHeading.text}</Heading>
        <CardGrid minCardWidth="220px">
          {ACTIVITIES.map((activity) => (
            <Card
              key={activity.slug}
              href={`/${activity.slug}`}
              title={`${activity.emoji} ${activity.title}`}
              interactive
              footer={activity.ready ? undefined : <Pill>{COPY.home.comingSoon.text}</Pill>}
            >
              <Text tone="muted">{activity.description}</Text>
            </Card>
          ))}
        </CardGrid>
      </Container>
    </SiteShell>
  );
}
