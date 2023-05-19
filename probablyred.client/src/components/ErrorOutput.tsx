const ErrorOutput = (props: { errors: string[] }) => {
  const { errors } = props;
  return (
    <div className="error">
      {errors.length ? (
        <div>
          <h4>Errors</h4>
          <p>For further details see console</p>
          {errors.map((err: string) => {
            return <div>{err}</div>;
          })}
        </div>
      ) : null}
    </div>
  );
};

export default ErrorOutput;
