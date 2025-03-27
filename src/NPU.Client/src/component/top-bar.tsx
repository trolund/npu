type TopBarProps = {
  searchTerm: string;
  setSearchTerm: (value: string) => void;
};

export const TopBar = ({ searchTerm, setSearchTerm }: TopBarProps) => {
  return (
    <div className="my-6 flex items-center">
      <div className="flex w-1/2 justify-end">
        <input
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="rounded-full border px-4 py-2"
          type="search"
          placeholder="Search"
          aria-label="Search"
        />
      </div>
    </div>
  );
};
