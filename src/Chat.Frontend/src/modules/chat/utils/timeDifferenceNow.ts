export const timeDifferenceNow = (date: string): number => {
  const parsedDate = new Date(Date.parse(date)).getTime();
  const now = new Date(Date.now()).getTime();
  const differenceInMinutes = (now - parsedDate) / 1000 / 60;
  return differenceInMinutes;
};
