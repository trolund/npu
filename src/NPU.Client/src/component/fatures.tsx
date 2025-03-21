import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { FunctionComponent } from "react";
import {
  faPlusCircle,
  faClock,
  faKey,
  faShield,
  faEraser,
  faCalendar,
} from "@fortawesome/free-solid-svg-icons";

interface FeaturesProps {}

const Features: FunctionComponent<FeaturesProps> = () => {
  return (
    <div className="rounded-lg bg-gray-900 p-8 shadow-lg">
      <h2 className="mb-4 text-2xl font-bold text-white">Lock Note Features</h2>
      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3">
        <div className="rounded-lg bg-gray-950 p-4 shadow">
          <FontAwesomeIcon icon={faPlusCircle} className="h-12" />
          <h3 className="text-lg font-semibold text-white">Create Notes</h3>
          <p className="text-gray-400">
            Generate a unique link for secure sharing.
          </p>
        </div>
        <div className="rounded-lg bg-gray-950 p-4 shadow">
          <FontAwesomeIcon icon={faClock} className="h-12" />
          <h3 className="text-lg font-semibold text-white">
            One-Time Readability
          </h3>
          <p className="text-gray-400">
            Notes are permanently deleted after being accessed.
          </p>
        </div>
        <div className="rounded-lg bg-gray-950 p-4 shadow">
          <FontAwesomeIcon icon={faKey} className="h-12" />
          <h3 className="text-lg font-semibold text-white">
            Password Protection (Optional)
          </h3>
          <p className="text-gray-400">Add an extra layer of security.</p>
        </div>
        <div className="rounded-lg bg-gray-950 p-4 shadow">
          <FontAwesomeIcon icon={faCalendar} className="h-12" />
          <h3 className="text-lg font-semibold text-white">Expiration Time</h3>
          <p className="text-gray-400">
            Notes automatically expire if not accessed.
          </p>
        </div>
        <div className="rounded-lg bg-gray-950 p-4 shadow">
          <FontAwesomeIcon icon={faShield} className="h-12" />
          <h3 className="text-lg font-semibold text-white">
            End-to-End Encryption
          </h3>
          <p className="text-gray-400">Ensures secure note storage.</p>
        </div>
        <div className="rounded-lg bg-gray-950 p-4 shadow">
          <FontAwesomeIcon icon={faEraser} className="h-12" />
          <h3 className="text-lg font-semibold text-white">
            Self-Destruct Mechanism
          </h3>
          <p className="text-gray-400">No data is retained after reading.</p>
        </div>
      </div>
    </div>
  );
};

export default Features;
