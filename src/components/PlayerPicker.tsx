"use client";

import { useState } from "react";
import { Button, Input, Select, Text } from "@noahwright/design";
import { addPlayer, setActivePlayer } from "@/lib/save/schema";
import { updateSave } from "@/lib/save/store";
import { useSave } from "./useSave";
import "./player-picker.css";

/** "Who's playing?": switch between players or add one. Results save to whoever is picked. */
export default function PlayerPicker() {
  const { save, player, loaded } = useSave();
  const [adding, setAdding] = useState(false);
  const [name, setName] = useState("");

  const add = (e: React.FormEvent) => {
    e.preventDefault();
    if (!name.trim()) return;
    updateSave((s) => addPlayer(s, name));
    setName("");
    setAdding(false);
  };

  if (!loaded) return null;

  if (adding || save.players.length === 0) {
    return (
      <form className="player-picker" onSubmit={add}>
        <Input label="Player name" value={name} onChange={(e) => setName(e.target.value)} />
        <Button type="submit" color="primary" disabled={!name.trim()}>
          Add player
        </Button>
        {save.players.length ? (
          <Button variant="text" onClick={() => setAdding(false)}>
            Cancel
          </Button>
        ) : (
          <Text tone="muted" inline>
            Or skip this: results save as &ldquo;Player 1&rdquo;.
          </Text>
        )}
      </form>
    );
  }

  return (
    <div className="player-picker">
      <Select
        label="Who's playing?"
        value={player?.id ?? ""}
        onChange={(e) => updateSave((s) => setActivePlayer(s, e.target.value))}
      >
        {save.players.map((p) => (
          <option key={p.id} value={p.id}>
            {p.name}
          </option>
        ))}
      </Select>
      <Button variant="outline" onClick={() => setAdding(true)}>
        + Add player
      </Button>
    </div>
  );
}
