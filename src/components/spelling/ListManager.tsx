"use client";

import { useRef, useState } from "react";
import NextLink from "next/link";
import { Button, Container, Heading, Input, Select, Text } from "@noahwright/design";
import SiteShell from "@/components/SiteShell";
import { downloadText } from "@/lib/download";
import { fillSentence } from "@/lib/spelling/quiz";
import {
  ListFileError,
  parseListFile,
  parseQuickList,
  serializeLists,
  slugify,
  uniqueId,
  type SpellingList,
} from "@/lib/spelling/lists";
import { useSpellingLists } from "./useSpellingLists";
import { COPY, fill } from "@/content/copy";
import "./spelling.css";

const EXAMPLE = `{
  "format": "level-up-learning.spelling-lists",
  "version": 1,
  "lists": [
    {
      "name": "My Week 1 Words",
      "grade": 2,
      "words": [
        { "word": "cake", "sentence": "Blow out the candles on your %% cake." },
        { "word": "rain" }
      ]
    }
  ]
}`;

type Notice = { tone: "success" | "error"; text: string } | null;

function downloadLists(lists: SpellingList[], name: string) {
  downloadText(`${slugify(name)}.json`, serializeLists(lists));
}

function ListRow({ list, onDelete }: { list: SpellingList; onDelete?: () => void }) {
  return (
    <li className="spelling-list-row">
      <details>
        <summary>
          <strong>{list.name}</strong> <Text tone="muted" inline>{list.words.length} words</Text>
        </summary>
        <ul className="spelling-list-row__words">
          {list.words.map((w) => (
            <li key={w.word}>
              <strong>{w.word}</strong>
              {w.sentence ? <> — {fillSentence(w.sentence, "___")}</> : null}
            </li>
          ))}
        </ul>
      </details>
      <div className="spelling-list-row__actions">
        <Button size="small" variant="outline" onClick={() => downloadLists([list], list.name)}>
          Download
        </Button>
        {onDelete ? (
          <Button size="small" variant="text" color="danger" onClick={onDelete}>
            Delete
          </Button>
        ) : null}
      </div>
    </li>
  );
}

/** /spelling/lists: upload, quick-create, download and delete word lists. */
export default function ListManager() {
  const { builtIn, custom, takenIds, addLists, removeList } = useSpellingLists();
  const [notice, setNotice] = useState<Notice>(null);
  const [quickName, setQuickName] = useState("");
  const [quickGrade, setQuickGrade] = useState("");
  const [quickWords, setQuickWords] = useState("");
  const fileRef = useRef<HTMLInputElement>(null);

  const save = (lists: SpellingList[]) => {
    if (!addLists(lists)) {
      setNotice({ tone: "error", text: COPY.spellingLists.notices.saveFailed.text });
      return false;
    }
    const names = lists.map((l) => `“${l.name}”`).join(", ");
    setNotice({ tone: "success", text: fill(COPY.spellingLists.notices.added, { names }) });
    return true;
  };

  const onFiles = async (files: FileList | null) => {
    if (!files?.length) return;
    const added: SpellingList[] = [];
    try {
      for (const file of [...files]) {
        try {
          added.push(...parseListFile(await file.text(), [...takenIds, ...added.map((l) => l.id)]));
        } catch (err) {
          throw new ListFileError(`${file.name}: ${err instanceof Error ? err.message : "couldn't be read."}`);
        }
      }
      save(added);
    } catch (err) {
      setNotice({ tone: "error", text: err instanceof Error ? err.message : "That file couldn't be read." });
    } finally {
      if (fileRef.current) fileRef.current.value = "";
    }
  };

  const onQuickCreate = (e: React.FormEvent) => {
    e.preventDefault();
    const words = parseQuickList(quickWords);
    if (!quickName.trim()) return setNotice({ tone: "error", text: COPY.spellingLists.notices.needName.text });
    if (!words.length) return setNotice({ tone: "error", text: COPY.spellingLists.notices.needWord.text });
    const list: SpellingList = {
      id: uniqueId(quickName, takenIds),
      name: quickName.trim(),
      grade: quickGrade ? Number(quickGrade) : undefined,
      words,
    };
    if (save([list])) {
      setQuickName("");
      setQuickGrade("");
      setQuickWords("");
    }
  };

  const onDelete = (list: SpellingList) => {
    if (window.confirm(fill(COPY.spellingLists.notices.confirmDelete, { name: list.name }))) {
      removeList(list.id);
      setNotice({ tone: "success", text: fill(COPY.spellingLists.notices.deleted, { name: list.name }) });
    }
  };

  return (
    <SiteShell>
      <Container padding="lg">
        <div className="spelling-setup">
          <div className="spelling-setup__intro">
            <NextLink href="/spelling" className="spelling-link">
              {COPY.spellingLists.backLink.text}
            </NextLink>
            <Heading level={1}>{COPY.spellingLists.heading.text}</Heading>
            <Text>
              {COPY.spellingLists.intro.before.text}{" "}
              <NextLink href="/progress" className="spelling-link">
                {COPY.spellingLists.intro.link.text}
              </NextLink>
              {COPY.spellingLists.intro.after.text}
            </Text>
          </div>

          {notice ? (
            <div className={`spelling-notice spelling-notice--${notice.tone}`} role="status">
              {notice.text}
            </div>
          ) : null}

          <section className="spelling-panel">
            <Heading level={2}>{COPY.spellingLists.uploadHeading.text}</Heading>
            <Text>{COPY.spellingLists.uploadIntro.text}</Text>
            <input
              ref={fileRef}
              type="file"
              accept=".json,application/json"
              multiple
              className="spelling-file"
              aria-label="Upload word list files"
              onChange={(e) => onFiles(e.target.files)}
            />
            <details className="spelling-format">
              <summary>{COPY.spellingLists.formatSummary.text}</summary>
              <Text>
                A list has a <code>name</code>, an optional <code>grade</code>, and <code>words</code>. Each word can
                have an example <code>sentence</code>; put <code>%%</code> where the word goes. A word can also be just
                a string, like <code>&quot;rain&quot;</code>.
              </Text>
              <pre>{EXAMPLE}</pre>
            </details>
          </section>

          <section className="spelling-panel">
            <Heading level={2}>{COPY.spellingLists.quickCreateHeading.text}</Heading>
            <form className="spelling-quick" onSubmit={onQuickCreate}>
              <div className="spelling-quick__row">
                <Input label="List name" value={quickName} onChange={(e) => setQuickName(e.target.value)} />
                <Select label="Grade" value={quickGrade} onChange={(e) => setQuickGrade(e.target.value)}>
                  <option value="">None</option>
                  {[1, 2, 3, 4, 5, 6, 7, 8].map((g) => (
                    <option key={g} value={g}>
                      Grade {g}
                    </option>
                  ))}
                </Select>
              </div>
              <Input
                label={COPY.spellingLists.wordsLabel.text}
                multiline
                rows={6}
                placeholder={"cake - Blow out the candles on your cake.\nrain - I hope it doesn't rain today.\nfriend"}
                value={quickWords}
                onChange={(e) => setQuickWords(e.target.value)}
              />
              <div>
                <Button type="submit" color="primary">
                  {COPY.spellingLists.createButton.text}
                </Button>
              </div>
            </form>
          </section>

          <section className="spelling-panel">
            <div className="spelling-panel__head">
              <Heading level={2}>{COPY.spellingLists.myListsHeading.text}</Heading>
              {custom.length > 1 ? (
                <Button size="small" variant="outline" onClick={() => downloadLists(custom, "my-spelling-lists")}>
                  Download all
                </Button>
              ) : null}
            </div>
            {custom.length ? (
              <ul className="spelling-list-rows">
                {custom.map((list) => (
                  <ListRow key={list.id} list={list} onDelete={() => onDelete(list)} />
                ))}
              </ul>
            ) : (
              <Text tone="muted">{COPY.spellingLists.noListsYet.text}</Text>
            )}
          </section>

          <section className="spelling-panel">
            <div className="spelling-panel__head">
              <Heading level={2}>{COPY.spellingLists.builtInHeading.text}</Heading>
              <Button size="small" variant="outline" onClick={() => downloadLists(builtIn, "level-up-spelling-lists")}>
                Download all
              </Button>
            </div>
            <ul className="spelling-list-rows">
              {builtIn.map((list) => (
                <ListRow key={list.id} list={list} />
              ))}
            </ul>
          </section>
        </div>
      </Container>
    </SiteShell>
  );
}
