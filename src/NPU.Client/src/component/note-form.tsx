import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import {
  faPlus,
  faMinus,
  faEye,
  faEyeSlash,
} from "@fortawesome/free-solid-svg-icons";
import { NoteDto } from "../types/NoteDto";
import {
  faSpinner,
  faLock,
  faLockOpen,
} from "@fortawesome/free-solid-svg-icons";
import cn from "classnames";

type Inputs = {
  message: string;
  numOfViews: number;
  password: string;
  passwordAgain: string;
};

type NoteFormProps = {
  mutate: (note: NoteDto) => void;
  isWaitingForCreation: boolean;
};

export default function NoteForm({
  mutate,
  isWaitingForCreation,
}: NoteFormProps) {
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm<Inputs>();

  const password = watch("password");
  const numOfViews = watch("numOfViews");

  const [message, setMessage] = useState<string>("");
  const [usePassword, setUsePassword] = useState<boolean>(false);
  const [ShowPassword, setShowPassword] = useState<boolean>(false);

  const onSubmit: SubmitHandler<Inputs> = () =>
    mutate({
      content: message,
      password: usePassword ? password : null,
      readBeforeDelete: numOfViews,
    } as NoteDto);

  return (
    /* "handleSubmit" will validate your inputs before invoking "onSubmit" */
    <form onSubmit={handleSubmit(onSubmit)}>
      <div className="flex flex-col gap-2">
        <label
          data-testid="message-label"
          htmlFor="message"
          className="mb-2 block text-sm font-medium text-gray-400"
        >
          Your message
        </label>
        <span className="z-10 -mb-4 ml-auto mr-6">
          <FontAwesomeIcon
            icon={usePassword ? faLock : faLockOpen}
            className={cn(
              "absolute",
              usePassword ? "text-slate-100" : "text-slate-500",
            )}
          />
        </span>

        <textarea
          data-testid="message"
          id="message"
          className="outline:ring-purple-700 block h-32 min-h-11 w-full rounded-lg border border-slate-700 bg-slate-950 bg-opacity-25 bg-gradient-to-tr from-purple-950/25 via-pink-950/10 to-red-950/10 p-2.5 text-sm text-white placeholder-gray-200 focus:border-purple-700 focus:ring-purple-700"
          placeholder="Your message..."
          {...register("message", {
            required: "The message field is required",
            maxLength: {
              value: 2000,
              message: "Max length is 2000 characters",
            },
            minLength: { value: 1, message: "Min length is 1 character" },
          })}
          onChange={(e) => setMessage(e.target.value)}
          value={message}
        ></textarea>
        <p className="text-red-500" data-testid="message-error">
          {errors.message && errors.message.message}
        </p>
      </div>

      <div className="mt-4 space-y-4 rounded-lg border-[1px] border-slate-700 p-4">
        <div className="flex w-full flex-col gap-2">
          <label className="text-sm font-medium text-gray-400">
            Number of views
          </label>
          <select
            data-testid="num-of-reads"
            title="number of reads"
            className="w-full rounded-lg border border-slate-700 bg-slate-950 p-2 text-sm text-white placeholder-gray-400 focus:border-blue-500 focus:ring-blue-500"
            {...register("numOfViews", {
              validate: (value) =>
                (value > 0 && value <= 10) ||
                "The number of views must be between 1 and 10",
            })}
            defaultValue={1}
          >
            {Array.from({ length: 10 }, (_, i) => (
              <option key={i + 1} value={i + 1}>
                {i + 1}
              </option>
            ))}
          </select>
        </div>
      </div>

      <div className="mt-4 space-y-4 rounded-lg border-[1px] border-slate-700 p-4">
        <button
          data-testid="expand-button"
          type="button"
          onClick={() => setUsePassword(!usePassword)}
          className="w-full bg-slate-900 text-sm text-blue-500 hover:underline"
        >
          <FontAwesomeIcon
            icon={usePassword ? faMinus : faPlus}
            className="mr-2"
          />
          {usePassword ? "Do not use password" : "Use password"}
        </button>
        {/* Expanding section */}
        {usePassword && (
          <div className="flex w-full flex-col gap-2">
            <label className="text-sm font-medium text-gray-400">
              Password
            </label>
            <span
              className="z-10 -mb-9 ml-auto mr-4"
              onClick={() => setShowPassword(!ShowPassword)}
            >
              <FontAwesomeIcon icon={ShowPassword ? faEyeSlash : faEye} />
            </span>
            <input
              type={ShowPassword ? "text" : "password"}
              className="w-full rounded-lg border border-slate-700 bg-slate-950 p-2 text-sm text-white placeholder-gray-400 focus:border-blue-500 focus:ring-blue-500"
              placeholder="Enter a password"
              {...register("password", { required: "Password is required" })}
            />
            <label className="text-sm font-medium text-gray-400">
              Password again
            </label>
            <span
              className="z-10 -mb-9 ml-auto mr-4"
              onClick={() => setShowPassword(!ShowPassword)}
            >
              <FontAwesomeIcon icon={ShowPassword ? faEyeSlash : faEye} />
            </span>
            <input
              type={ShowPassword ? "text" : "password"}
              className="w-full rounded-lg border border-slate-700 bg-slate-950 p-2 text-sm text-white placeholder-gray-400 focus:border-blue-500 focus:ring-blue-500"
              placeholder="Enter the same again password"
              {...register("passwordAgain", {
                validate: (value) =>
                  value === password || "Passwords do not match",
              })}
            />
            <p className="text-red-500">
              {errors.passwordAgain && errors.passwordAgain.message}
            </p>
            <hr className="border-dashed border-slate-700" />
            <p className="text-slate-500">
              When a password is used is the message is encrypted and can not be
              read by anyone without the correct password.
            </p>
          </div>
        )}
      </div>

      <button
        data-testid="submit-button"
        type="submit"
        className="ml-auto mr-auto mt-5 flex gap-3 rounded-lg bg-blue-500 px-4 py-2 align-middle text-sm font-medium text-white hover:bg-blue-600"
      >
        {isWaitingForCreation && (
          <FontAwesomeIcon icon={faSpinner} className="animate-spin" />
        )}
        Create note
      </button>
    </form>
  );
}
