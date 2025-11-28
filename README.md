# Bindings

`.NET Aspire` + `DAPR` - Bindings

## For a written article, refer: [Bindings](https://netrecipes.github.io/courses/dapr-aspire/bindings/)

## Input Bindings

- [x] `CRON`

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: scheduler
  namespace: default
spec:
  type: bindings.cron
  version: v1
  metadata:
  - name: schedule
    value: "@every 20s"
  - name: route
    value: /api/bindings/cron
```

---

## Output Bindings

- [x] `Local Storage`

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: storage
spec:
  type: bindings.localstorage
  version: v1
  metadata:
  - name: rootPath
    value: "../Storage"
```

---
