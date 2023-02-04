export function formatDate(dateStr: string): string {
  const date = new Date(dateStr);

  const dateStrFormatted =
    date.getFullYear() +
    '.' +
    ('00' + date.getDate()).slice(-2) +
    '.' +
    ('00' + (date.getMonth() + 1)).slice(-2) +
    ' ' +
    ('00' + date.getHours()).slice(-2) +
    ':' +
    ('00' + date.getMinutes()).slice(-2);
  return dateStrFormatted;
}
