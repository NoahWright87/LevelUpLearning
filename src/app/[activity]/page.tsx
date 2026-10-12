import type { Metadata } from "next";
import { notFound } from "next/navigation";
import ActivityPlaceholder from "@/components/ActivityPlaceholder";
import { ACTIVITIES } from "@/lib/site";

type Params = { activity: string };

export function generateStaticParams(): Params[] {
  return ACTIVITIES.map((a) => ({ activity: a.slug }));
}

export const dynamicParams = false;

function findActivity(slug: string) {
  return ACTIVITIES.find((a) => a.slug === slug);
}

export async function generateMetadata({ params }: { params: Promise<Params> }): Promise<Metadata> {
  const activity = findActivity((await params).activity);
  return activity ? { title: activity.title, description: activity.description } : {};
}

/** Placeholder route for each activity until it gets a real page of its own. */
export default async function ActivityPage({ params }: { params: Promise<Params> }) {
  const activity = findActivity((await params).activity);
  if (!activity) notFound();
  return <ActivityPlaceholder slug={activity.slug} />;
}
