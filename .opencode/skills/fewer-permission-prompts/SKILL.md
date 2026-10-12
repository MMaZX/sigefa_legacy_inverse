---
name: fewer-permission-prompts
description: "Trigger: fewer prompts, permission allowlist, reduce approval prompts. Scan opencode sessions and add read-only bash rules to opencode.json."
license: Apache-2.0
metadata:
  author: gentleman-programming
  version: "1.0"
---

## Activation Contract

Use when the user asks to reduce permission prompts, build a bash allowlist, or audit frequent read-only tool calls in opencode. Do not run for a one-off approval.

## Hard Rules

- Read-only only. Never allowlist a wildcard that grants arbitrary code execution: interpreters (`python*`, `node`, `bun`, `deno`, `ruby`, `perl`, `php`, `lua`), shells (`bash`, `sh`, `zsh`, `fish`, `eval`, `exec`, `ssh`), runners (`npx`, `bunx`, `uvx`, `uv run`), task wildcards (`npm run *`, `bun run *`, `make *`, `just *`, `cargo run *`, `go run *`), `gh api *`, `docker exec`/`docker run`, `kubectl exec`, `sudo`. An exact form like `bun run typecheck` is fine; `bun run *` is not.
- Project config only: merge into `./opencode.json`, or `.opencode/opencode.json` when that file already exists. Never touch `~/.config/opencode/opencode.json` or agent-specific blocks unless explicitly asked.
- Opencode ordering wins: inside `permission.bash`, `"*"` stays first and specifics go after, because the last matching rule takes precedence.
- Never write `permissions.deny`, `permissions.ask`, or any non-`permission` field. Preserve existing keys, de-duplicate, remove nothing.
- Opencode has no Claude-style auto-allow source file. Never invent one; the only "already allowed" source is the current `permission` config.

## Decision Gates

| Situation | Action |
| --- | --- |
| `~/.local/share/opencode/opencode.db` missing or unreadable | Stop and report; do not guess frequencies |
| Project `permission.bash` is already `"allow"` wholesale | Skip bash rules, report it, continue with non-bash tools only |
| Candidate appeared fewer than ~3 times | Drop it; cap the final list at ~20 |
| Exact invocation vs many variants | Single form keeps exact string; variants get `"prefix *": "allow"` |

## Execution Steps

1. Scan recent opencode sessions (cap ~50 most-recent sessions). Tool calls live in sqlite `~/.local/share/opencode/opencode.db`, table `part`, JSON rows with `type: "tool"`, `tool: "<name>"`, `state.input` (for `bash`, `state.input.command` is the shell string). Join `session` for recency via `time_updated DESC LIMIT 50`.
2. Count frequencies. For `bash`, parse `command`, take leading token plus first subcommand (strip `sudo`, `timeout`, pipes, `&&`, env prefixes): `git status`, `gh pr view`, `ls`, `rg`. For every other tool record the full tool name (`read`, `grep`, `glob`, `webfetch`, `engram_mem_search`).
3. Keep read-only only (`ls`, `cat`, `git status/log/diff/show/branch`, `gh pr/issue/run view/list/diff`, `rg`, `grep`, `find`, `head`, `tail`, `docker ps/logs`, `kubectl get/describe`, `ps`, `df`, `read`/`glob`/`grep` tools, `*read*`/`*get*`/`*list*`/`*search*`/`*view*` MCP tools). Drop writes, deletes, pushes, merges, installs, side-effecting builds. When in doubt leave it out.
4. Drop anything the current project plus global `permission` config already allows. Read both `opencode.json` files first.
5. Rank by count descending, present a markdown table (`# | opencode.json rule | count | notes`, e.g. `"git status *": "allow"` — 142 — repo status checks).
6. Merge the top rules into the project `opencode.json` under `permission.bash` (bash patterns) and top-level tool keys (`"read": "allow"`, `"grep": "allow"`, `"glob": "allow"`, `"<mcp_tool>": "allow"`). Keep `"*": "ask"` first when present; append specifics after.

## Output Contract

Return:
- Project `opencode.json` path created or modified.
- Markdown table shown to the user.
- Count added, count already allowed, and what was skipped and why (not read-only, arbitrary-exec risk, below threshold).

## References

- `https://opencode.ai/docs/permissions` — granular `permission` rules, last-match-wins ordering.
- `https://opencode.ai/docs/agents` — per-agent vs global `permission` scope.
