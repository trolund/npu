import { useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { NpuResponse } from "../types/NpuResponse";
import { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { TopBar } from "../component/top-bar";
import Modal from "../component/modal";

export default function NpuOverview() {
  const [searchTerm, setSearchTerm] = useState("");
  const navigate = useNavigate();

  const [showModal, setShowModal] = useState(false);

  const handleOpenModal = () => {
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
  };

  // allow for dynamic columns
  const columns = useMemo<ColumnDef<NpuResponse>[]>(
    () => [
      {
        accessorKey: "userId",
        header: "Owner",
        cell: (info) => info.getValue(),
        footer: (props) => props.column.id,
      },
      {
        accessorKey: "customerId",
        header: "Customer",
        cell: (info) => info.getValue(),
        footer: (props) => props.column.id,
      },
      {
        accessorKey: "name",
        header: "Name",
        cell: (info) => info.getValue(),
        footer: (props) => props.column.id,
      },
      {
        accessorKey: "deadline",
        header: "Deadline",
        cell: (info) => format(new Date(info.getValue() as string), "dd-MM-yy"),
        footer: (props) => props.column.id,
      },
    ],
    [],
  );

  const rowClickHandler = (p: NpuResponse) => {
    navigate(`project/${p.id}`);
  };

  return (
    <>
      <Modal show={showModal} onClose={handleCloseModal} title="Add project">
        {/* <TimeForm /> */}
        <p></p>
      </Modal>
      <TopBar searchTerm={searchTerm} setSearchTerm={setSearchTerm} />
      {/* <Table
        dataFetcher={useGetPaginatedNpus}
        onRowClick={rowClickHandler}
        columns={columns}
        searchTerm={searchTerm}
      /> */}
    </>
  );
}
