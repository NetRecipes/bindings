# Bindings

`.NET Aspire` + `DAPR` - Bindings

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
