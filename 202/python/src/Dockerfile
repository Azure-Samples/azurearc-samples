FROM python:3.10-alpine
ENV AZURE_TENANT_ID="<TenantId>"
ENV AZURE_CLIENT_ID="<ClientId>"
ENV AZURE_CLIENT_SECRET="<ClientSecret>"
WORKDIR /src
COPY Program.py .
RUN pip install --no-cache-dir --upgrade pip \
  && pip install --no-cache-dir azure-identity azure.mgmt.kubernetesconfiguration
CMD ["python", "-u", "Program.py"]